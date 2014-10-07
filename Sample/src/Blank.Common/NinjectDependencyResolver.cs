using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject;
using System.Web.Http.Dependencies;

namespace Blank.Common
{
    public sealed class NinjectDependencyResolver : IDependencyResolver
    {
        #region Fields

        private readonly IKernel _container;

        #endregion

        #region Ctor

        public NinjectDependencyResolver(IKernel container)
        {
            _container = container;
        }

        #endregion

        #region Private Methods
        #endregion

        #region Public Methods

        

        #endregion

        #region Protected Methods
        #endregion

        #region Dependency Properties
        #endregion

        #region Properties

        public IKernel Container
        {
            get { return _container; }
        }

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

        #region IDependencyResolver Implementations

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            return _container.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAll(serviceType);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}