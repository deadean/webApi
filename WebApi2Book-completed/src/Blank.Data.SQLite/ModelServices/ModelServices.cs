using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Blank.Data.Implementations.Entities;
using Blank.Data.Interfaces.Entities;
using ViewModelLib.Commands;
using WebApi.Data.Implementations.Entities;

namespace Blank.Data.SQLite.ModelServices
{
    public class ModelServices:IModelServices
    {
        #region Fields

        private static IModelServices modInstance;
        private ModelContext modDbContext = new ModelContext();

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

        public List<T> GettEntities<T>() where T : IEntity
        {
            if (typeof(T) == typeof(User)) return modDbContext.USER.OfType<T>().ToList();
            if (typeof(T) == typeof(Status)) return modDbContext.STATUS.OfType<T>().ToList();

            return null;
        }

        public void SaveChanges()
        {
            this.modDbContext.SaveChanges();
        }

        public void AddNewEntity(IEntity entity)
        {
            var dict = new Dictionary<Type, Action<IEntity>>() 
            { 
                {typeof(User),(x)=> this.modDbContext.USER.Add(x as User)},
                {typeof(Status),(x)=> this.modDbContext.STATUS.Add(x as Status)}
            };
            entity.With(x => dict.If(y => y.ContainsKey(x.GetType()), y => y[x.GetType()].Invoke(x)));
            this.modDbContext.SaveChanges();
        }

        public void UpdateEntity(IEntity entity)
        {
            this.TryCatch(() =>
            {
                var dict = new Dictionary<Type, Action<IEntity>>() 
                { 
                    { typeof(User),(x) => 
                        { 
                            this.modDbContext.USER.Find(x.Id).With(y => modDbContext.Entry(y).CurrentValues.SetValues(x));
                            if (this.modDbContext.USER.Find(x.Id) == null)
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
                var dict = new Dictionary<Type, Action<IEntity>>() 
                { 
                    { typeof(User),(x) => this.modDbContext.USER.Find(x.Id).With(y => modDbContext.USER.Remove((User)x))}
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
