using System.Collections.Generic;
using Blank.Data.Interfaces.Entities;

namespace WebApi.DataBase.Sqlite.EnterpriseLibrary.ModelServices
{
	public class ModelServices : IModelServices
	{
		#region Fields

		private static IModelServices modInstance;
		private ModelContext modDbContext = new ModelContext();

		#endregion

		#region Properties

		#endregion

		#region Ctor

		private ModelServices()
		{
		}

		#endregion

		#region Public Services

		public static IModelServices GetInstance()
		{
			return modInstance ?? (modInstance = new ModelServices());
		}

		public IEnumerable<T> GetEntities<T>() where T : class, new()
		{
			return modDbContext.Get<T>();
		}

		#endregion

	}
}
