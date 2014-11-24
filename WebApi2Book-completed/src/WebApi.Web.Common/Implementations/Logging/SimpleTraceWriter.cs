using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Tracing;
using WebApi.Web.Interfaces.Logging;

namespace WebApi.Web.Common.Implementations.Logging
{
    public class SimpleTraceWriter : ITraceWriter
    {
        private readonly ILog _log;

        public SimpleTraceWriter(ILogManager logManager)
        {
            _log = logManager.GetLog(typeof(SimpleTraceWriter));
        }

        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            //var rec = new TraceRecord(request, category, level);
            //traceAction(rec);
            //WriteTrace(rec);
        }

        public virtual void WriteTrace(TraceRecord rec)
        {
            string traceFormat =
                Environment.NewLine +
                "=================================================================================================================" + Environment.NewLine +
                "RequestId={0};{1}Kind={2};{3}Status={4};{5}Operation={6};{7}Operator={8};{9}Category={10}{11}Request={12}{13}Message={14}";

            var args = new object[]
            {
                rec.RequestId,
                Environment.NewLine,
                rec.Kind,
                Environment.NewLine,
                rec.Status,
                Environment.NewLine,
                rec.Operation,
                Environment.NewLine,
                rec.Operator,
                Environment.NewLine,
                rec.Category,
                Environment.NewLine,
                rec.Request,
                Environment.NewLine,
                rec.Message
            };

            switch (rec.Level)
            {
                case TraceLevel.Debug:
                    _log.DebugFormat(traceFormat, args);
                    break;
                case TraceLevel.Info:
                    _log.InfoFormat(traceFormat, args);
                    break;
                case TraceLevel.Warn:
                    _log.WarnFormat(traceFormat, args);
                    break;
                case TraceLevel.Error:
                    _log.ErrorFormat(traceFormat, args);
                    break;
                case TraceLevel.Fatal:
                    _log.FatalFormat(traceFormat, args);
                    break;
            }
        }
    }
}
