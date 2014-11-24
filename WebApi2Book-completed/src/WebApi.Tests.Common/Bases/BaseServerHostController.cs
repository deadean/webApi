using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Tests.Common.Bases
{
	public class BaseServerHostController : BaseUnitTestController
	{
		public BaseServerHostController()
		{
			this.modBaseAddress = WebApi.Web.Data.Implementations.Constants.Constants.csServerHostAddress;
			this.Initialize();
			
		}
		protected override void Initialize()
		{
			this.modClient.BaseAddress = new Uri(modBaseAddress);
		}

		
	}
}
