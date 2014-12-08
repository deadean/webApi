using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApi.Tests.Common.Implementations;
using WebApi.Tests.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApi.Tests.Common.Bases
{
	public abstract class BaseUnitTestController
	{
		protected HttpClient modClient = new HttpClient();
		protected WebClientHelper modClientHelper = new WebClientHelper();
		protected string modBaseAddress;
		protected string modAddress;
		private readonly IEmailService modEmailService = EmailService.GetInstance();

		protected virtual void Initialize() { }
		protected void RunTest(Action action)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				modEmailService.AddBadTest();
				throw ex;
			}
		}

		[TestCleanup()]
		public void Cleanup() 
		{
			//modEmailService.SendTestReport();
		}
	}
}
