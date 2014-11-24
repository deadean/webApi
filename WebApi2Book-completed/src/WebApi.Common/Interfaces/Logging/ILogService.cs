using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Common.Interfaces.Logging
{
	public interface ILogService
	{
		void Debug(string message);
		void Debug(string message, Exception ex);
		void DebugFormat(string formatMessage, params object[] args);
	}
}
