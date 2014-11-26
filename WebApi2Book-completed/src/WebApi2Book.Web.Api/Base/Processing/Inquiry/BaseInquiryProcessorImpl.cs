using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Common.Implementations.Factories;

namespace WebApi.WebApiService.Bases.Processing.Inquiry
{
	public class BaseInquiryProcessorImpl : BaseInquiryProcessor
	{

		public BaseInquiryProcessorImpl()
		{
			modObjectsByTypeFactory = ObjectsByTypeFactory.GetFactory();
			InitLog();
		}

		protected override void InitLog()
		{
			modLog = this.GetLog<BaseInquiryProcessorImpl>();
		}
	}
}