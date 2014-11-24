using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Common.Interfaces.Logging;
using WebApi.Web.Interfaces.Logging;

namespace WebApi.Common.Implementations.Logging
{
	public class LogService : ILogService
	{
		private static readonly ILogManager logManager = new LogManagerAdapter();
		private readonly ILog modLog;

		LogService(ILog log)
		{
			modLog = log;
		}

		public static ILogService GetLogService<T>() where T : class
		{
			return new LogService(logManager.GetLog(typeof(T)));
		}

		public void Debug(string message)
		{
			modLog.Debug(message);
		}


		public void DebugFormat(string formatMessage, params object[] args)
		{
			modLog.DebugFormat(formatMessage, args);
		}


		public void Debug(string message, Exception ex)
		{
			modLog.Error(message, ex);
		}
	}
}
