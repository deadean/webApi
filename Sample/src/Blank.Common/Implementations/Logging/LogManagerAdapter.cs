using Blank.Common.Interfaces.Logging;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blank.Common.Implementations.Logging
{
    public class LogManagerAdapter : ILogManager
    {
        public log4net.ILog GetLog(Type typeAssociatedWithRequestedLog)
        {
            var log = LogManager.GetLogger(typeAssociatedWithRequestedLog);
            return log;

        }
    }
}
