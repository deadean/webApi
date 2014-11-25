using System;
using WebApi.Common.Enumerations;
using WebApi.Common.Implementations.Logging;
using WebApi.Common.Implementations.Unity;
using WebApi.DataBase.Sqlite.EnterpriseLibrary.ModelServices;

namespace WebApi.DataBase.Sqlite.EnterpriseLibrary.Bases.QueryProcessors
{
	public class QueryProcessorBase : WebApi.Common.Bases.QueryProcessors.QueryProcessorBase
	{
		#region Fields

		protected IModelServices modModelService;

		#endregion

		static QueryProcessorBase()
		{
			Container.RegisterType(typeof(IModelServices), typeof(ModelServices.ModelServices), enTypeLifeTime.Singleton);
		}

		public QueryProcessorBase()
		{
			try
			{
				InitLog();

				modModelService = Container.Resolve<IModelServices>();
			}
			catch (Exception ex)
			{
				modLog.Debug(ex.Message);
			}
		}

		public override sealed void InitLog()
		{
			modLog = LogService.GetLogService<QueryProcessorBase>();
		}

	}
}
