using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Tests.Common.Bases
{
	public class BaseLocalHostController : BaseUnitTestController
	{
		public BaseLocalHostController()
		{
			this.modBaseAddress = WebApi.Web.Data.Implementations.Constants.Constants.csLocalHostAddress;
			this.Initialize();
		}
		protected override void Initialize()
		{
			this.modClient.BaseAddress = new Uri(modBaseAddress);
		}
	}
}
