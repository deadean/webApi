﻿using EntitySqlite;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLib.Commands;

namespace Model.ModelServices
{
    public class ModelServices:IModelServices
    {
        #region Fields

        private static IModelServices modInstance;
        private ModelContext modDbContext = new ModelContext();
        private string connectionString = @"data source=";

        const string csDataBaseName = "dataBase";

        #endregion

        #region Properties

        public bool IsUseEntityFrameWork { get; set; }

        #endregion

        #region Public Services

        public ModelServices()
        {
            IsUseEntityFrameWork = true;
        }

        public static IModelServices GetInstance()
        {
            if (modInstance == null)
                modInstance = new ModelServices() { IsUseEntityFrameWork = true };
            return modInstance;
        }

        public List<User> GetAllUsers()
        {
            return modDbContext.USER.ToList();
        }

        public void SaveChanges()
        {
            this.modDbContext.SaveChanges();
        }

        public void AddNewEntity(IEntity entity)
        {
            Dictionary<Type, Action<IEntity>> dict = new Dictionary<Type, Action<IEntity>>() 
            { 
                {typeof(User),(x)=> this.modDbContext.USER.Add(x as User)}
            };
            entity.With(x => dict.If(y => y.ContainsKey(x.GetType()), y => y[x.GetType()].Invoke(x)));
            this.modDbContext.SaveChanges();
        }

        public void UpdateEntity(IEntity entity)
        {
            this.TryCatch(() =>
            {
                Dictionary<Type, Action<IEntity>> dict = new Dictionary<Type, Action<IEntity>>() 
                { 
                    { typeof(Product),(x) => 
                        { 
                            this.modDbContext.USER.Find(x.ID).With(y => modDbContext.Entry(y).CurrentValues.SetValues(x));
                            if (this.modDbContext.USER.Find(x.ID) == null)
                                AddNewEntity(x);
                        }}
                };
                entity.With(x => dict.If(y => y.ContainsKey(x.GetType()), y => y[x.GetType()].Invoke(x)));
                this.modDbContext.SaveChanges();
            }, "UpdateEntity");
        }

        public void RemoveEntity(IEntity entity)
        {
            this.TryCatch(() =>
            {
                Dictionary<Type, Action<IEntity>> dict = new Dictionary<Type, Action<IEntity>>() 
                { 
                    { typeof(Product),(x) => this.modDbContext.USER.Find(x.ID).With(y => modDbContext.USER.Remove((User)x))}
                };
                entity.With(x => dict.If(y => y.ContainsKey(x.GetType()), y => y[x.GetType()].Invoke(x)));
                this.modDbContext.SaveChanges();
            }, "RemoveEntity");
        }

        public string GetAppSettingsDataBase()
        {
            return ConfigurationManager.AppSettings[csDataBaseName];
        }
        
        #endregion
    
    }
}
