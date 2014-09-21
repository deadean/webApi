using System.Web.Http;

namespace Blank.Web.Common.Routing
{
    public class ApiVersion1RoutePrefixAttribute:RoutePrefixAttribute
    {
        private const string RouteBase = "api/{apiVersion:apiVersionConstraint()v1}";
        private const string PrefixBase = RouteBase + "/";

        public ApiVersion1RoutePrefixAttribute(string routePrefix)
            : base(string.IsNullOrWhiteSpace(routePrefix) ? RouteBase : RouteBase + routePrefix)
        {
            
        }
    }
}
