using Blank.Common.Implementations;
using Blank.Common.Implementations.Logging;
using Blank.Common.Interfaces;
using Blank.Common.Interfaces.Logging;
using log4net.Config;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blank.Web.Api.App_Start
{
    public class NinjectConfigurator
    {
        #region Fields
        #endregion

        #region Ctor
        #endregion

        #region Private Methods

        private void AddBindings(IKernel container)
        {
            ConfigureLog4net(container);
            container.Bind<IDateTime>().To<DateTimeAdapter>().InSingletonScope();
        }

        private void ConfigureLog4net(IKernel container)
        {
            XmlConfigurator.Configure();
            var logManager = new LogManagerAdapter();
            container.Bind<ILogManager>().ToConstant(logManager);
        }

        #endregion

        #region Public Methods

        public void Configure(IKernel container)
        {
            AddBindings(container);
        }

        #endregion

        #region Protected Methods
        #endregion

        #region Dependency Properties
        #endregion

        #region Properties
        #endregion

        #region Commands
        #endregion

        #region Commands Execute Handlers
        #endregion

        #region Commands Can Execute Handlers
        #endregion

        #region Message Handlers
        #endregion

        #region Events Handlers
        #endregion

    }
}