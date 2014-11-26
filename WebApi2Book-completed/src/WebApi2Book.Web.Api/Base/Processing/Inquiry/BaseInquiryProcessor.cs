using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Common.Implementations.Factories;
using WebApi.Common.Implementations.Logging;
using WebApi.Common.Interfaces.Factories;
using WebApi.Common.Interfaces.Logging;

namespace WebApi.WebApiService.Bases.Processing.Inquiry
{
	public abstract class BaseInquiryProcessor
	{
		#region Fields

		protected IObjectsByTypeFactory modObjectsByTypeFactory;
		protected ILogService modLog;

		#endregion

		#region Ctor

		#endregion

		#region Private Methods
		#endregion

		#region Public Methods
		#endregion

		#region Protected Methods

		protected abstract void InitLog();

		protected ILogService GetLog<T>() where T : class
		{
			return LogService.GetLogService<T>();
		}

		#endregion

		#region Properties
		#endregion
	}
}