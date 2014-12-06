using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebApi.Common.Implementations.Logging;
using WebApi.Common.Interfaces.Logging;

namespace WebApi.Web.Common.Implementations.FilterAttributes
{
	public sealed class UnitOfWorkAttribute : ActionFilterAttribute
	{
		#region Fields

		private readonly ILogService modlog = LogService.GetLogService<UnitOfWorkAttribute>();
		private System.Diagnostics.Stopwatch modStopWatch = new System.Diagnostics.Stopwatch();

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
