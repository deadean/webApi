using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using Blank.Web.Common;
using Blank.Web.Common.Routing;
using System.ServiceModel;

namespace Blank.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.HostNameComparisonMode = HostNameComparisonMode.Exact;
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{namespace}/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );

            config.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(config));

            //var constraintsResolver = new DefaultInlineConstraintResolver();
            //constraintsResolver.ConstraintMap.Add("apiVersionConstraint", typeof
            //(ApiVersionConstraint));
            //config.MapHttpAttributeRoutes(constraintsResolver);
            //config.Services.Replace(typeof(IHttpControllerSelector),
            //new NamespaceHttpControllerSelector(config));

            ////config.Routes.MapHttpRoute(
            ////    name: "FindByTaskNumberRoute",
            ////    routeTemplate: "api/{controller}/{taskNum}",
            ////    defaults: new { taskNum = RouteParameter.Optional }
            ////);

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/v2/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
