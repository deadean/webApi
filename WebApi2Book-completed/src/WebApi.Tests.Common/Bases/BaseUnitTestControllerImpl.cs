using NinjectService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Web.Common.Implementations.Security;
using WebApi.Web.Common.Interfaces.Security;

namespace WebApi.Tests.Common.Bases
{
	public class BaseUnitTestControllerImpl : BaseUnitTestController
	{
		protected readonly INinjectService modNinjectService = new NinjectService.NinjectService();
		public BaseUnitTestControllerImpl()
		{
			modNinjectService.Register<IJsonSecurityTokenGenerator, JsonWebSecurityTokenGenerator>();
		}
		protected override void Initialize()
		{
			base.Initialize();
		}
	}
}
