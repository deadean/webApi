using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Blank.Data.Implementations.Entities;
using Blank.Data.Interfaces.Entities;
using ViewModelLib.Commands;
using WebApi.DataBase.Oracle.EnterpriseLibrary.ModelServices;

namespace WebApi.DataBase.Sqlite.EnterpriseLibrary.ModelServices
{
    public class ModelServices:IModelServices
    {
        #region Fields

        private static IModelServices modInstance;
        private ModelContext modDbContext = new ModelContext();

        #endregion

        #region Properties

        #endregion

        #region Public Services

        public ModelServices()
        {
        }

        public static IModelServices GetInstance()
        {
					if (modInstance == null)
						modInstance = new ModelServices();
           return modInstance;
        }

        public List<T> GettEntities<T>() where T : IEntity
        {
						//if (typeof(T) == typeof(User)) return modDbContext.USER.OfType<T>().ToList();
						//if (typeof(T) == typeof(Status)) return modDbContext.STATUS.OfType<T>().ToList();

            return null;
        }

        #endregion
    
    }
}
