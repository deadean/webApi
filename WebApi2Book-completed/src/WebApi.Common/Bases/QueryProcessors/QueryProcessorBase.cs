using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Common.Enumerations;
using WebApi.Common.Implementations.Logging;
using WebApi.Common.Implementations.Unity;
using WebApi.Common.Interfaces.Logging;

namespace WebApi.Common.Bases.QueryProcessors
{
	public abstract class QueryProcessorBase
	{
		#region Fields

		protected ILogService modLog;

		#endregion

		public QueryProcessorBase()
		{
			try
			{
				InitLog();
			}
			catch (Exception ex)
			{
				modLog.Debug(ex.Message);
			}
		}

		public virtual void InitLog()
		{
			modLog = LogService.GetLogService<QueryProcessorBase>();
		}

		protected ILogService GetLog<T>() where T : class
		{
			return LogService.GetLogService<T>();
		}
	}
}
