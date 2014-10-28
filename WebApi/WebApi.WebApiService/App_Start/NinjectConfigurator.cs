using log4net.Config;
using Ninject;
using Ninject.Activation;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Web.Common.Implementations.Logging;
using WebApi.Web.Common.Interfaces.Logging;
using WebApi.WebApiService.Implementations.DependencyBlock;
using WebApi.WebApiService.Interfaces.DependencyBlock;

namespace WebApi.WebApiService.App_Start
{
    public class NinjectConfigurator
    {
        /// <summary>
        ///     Entry method used by caller to configure the given
        ///     container with all of this application's
        ///     dependencies.
        /// </summary>
        public void Configure(IKernel container)
        {
            AddBindings(container);
        }

        private void AddBindings(IKernel container)
        {
            ConfigureLog4net(container);

            container.Bind<IUsersDependencyBlock>().To<UsersDependencyBlock>().InRequestScope();
        }

        private void ConfigureLog4net(IKernel container)
        {
            XmlConfigurator.Configure();

            var logManager = new LogManagerAdapter();
            container.Bind<ILogManager>().ToConstant(logManager);
        }
    }
}