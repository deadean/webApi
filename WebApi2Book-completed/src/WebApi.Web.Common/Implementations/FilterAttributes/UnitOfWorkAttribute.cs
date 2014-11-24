using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebApi.Common.Implementations.Formatting.Json;
using WebApi.Common.Implementations.Logging;
using WebApi.Common.Interfaces.Formatting.Json;
using WebApi.Common.Interfaces.Logging;
using WebApi.Web.Interfaces.Logging;

namespace WebApi.Web.Common.Implementations.FilterAttributes
{
	public sealed class UnitOfWorkAttribute : ActionFilterAttribute
	{
		#region Fields

		private readonly ILogService modlog = LogService.GetLogService<UnitOfWorkAttribute>();
		private System.Diagnostics.Stopwatch modStopWatch = new System.Diagnostics.Stopwatch();
		private readonly IJsonService modJsonService = new JsonService();

		#endregion

		#region Ctor

		#endregion

		#region Private Methods
		#endregion

		#region Public Methods
		#endregion

		#region Protected Methods

		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			modStopWatch.Reset();
			modStopWatch.Start();
			modlog.DebugFormat("Request is starting performing..\n Uri : {0},\n Method : {1}", actionContext.Request.RequestUri, actionContext.Request.Method);
		}

		public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
		{
			try
			{
				modStopWatch.Stop();
				modlog.DebugFormat("Request has finished. Executed time : {0} ms \n Response : {1}", modStopWatch.ElapsedMilliseconds, actionExecutedContext.Response.Content.ReadAsStringAsync().Result);
			}
			catch (Exception ex)
			{
				modlog.Debug("OnActionExecuted", ex);
			}
		}

		#endregion

		#region Properties

		public override bool AllowMultiple
		{
			get { return false; }
		}

		#endregion

	}
}
