using System;

namespace WebApi.Tests.Common.Bases
{
	public class BaseLocalHostController : BaseUnitTestControllerImpl
	{
		public BaseLocalHostController()
		{
			this.modBaseAddress = WebApi.Web.Data.Implementations.Constants.Constants.csLocalHostAddress;
			this.Initialize();
		}
		protected override void Initialize()
		{
			base.Initialize();
			this.modClient.BaseAddress = new Uri(modBaseAddress);
		}
	}
}
