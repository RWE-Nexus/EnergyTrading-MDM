using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using MDM.ServiceHost.WebApi.Infrastructure.Controllers;

namespace MDM.ServiceHost.WebApi
{
    /// <summary>
    /// Configures the routes supported by the dynamic controller mechanism used by the MDM service
    /// </summary>
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}",
                defaults: new { },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) }
            );

            config.Routes.MapHttpRoute(
                name: "VersionedDefaultApi",
                routeTemplate: "{version}/{controller}",
                defaults: new { },
                constraints: new { version = @"v\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Post) }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultIdApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get, HttpMethod.Put, HttpMethod.Post) } // TODO shouldn't really support POST but MDM Client sends updates as POST currently
            );

            config.Routes.MapHttpRoute(
                name: "VersionedDefaultIdApi",
                routeTemplate: "{version}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { version = @"v\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Get, HttpMethod.Put, HttpMethod.Post) } // TODO shouldn't really support POST but MDM Client sends updates as POST currently
            );

            config.Routes.MapHttpRoute(
                name: "MappingApi",
                routeTemplate: "{controller}/{id}/mapping/{mappingid}",
                defaults: new { mappingid = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "VersionedMappingApi",
                routeTemplate: "{version}/{controller}/{id}/mapping/{mappingid}",
                defaults: new { mappingid = RouteParameter.Optional },
                constraints: new { version = @"v\d+" }
            );

            config.Routes.MapHttpRoute(
                name: "MappingsApi",
                routeTemplate: "{controller}/{id}/mappings",
                defaults: new { },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: "VersionedMappingsApi",
                routeTemplate: "{version}/{controller}/{id}/mappings",
                defaults: new { },
                constraints: new { version = @"v\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: "ListApi",
                routeTemplate: "{controller}/list",
                defaults: new { },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: "VersionedListApi",
                routeTemplate: "{version}/{controller}/list",
                defaults: new { },
                constraints: new { version = @"v\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: "EntityListApi",
                routeTemplate: "{controller}/{id}/list",
                defaults: new { },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: "VersionedEntityListApi",
                routeTemplate: "{version}/{controller}/{id}/list",
                defaults: new { },
                constraints: new { version = @"v\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: "EntityMapApi",
                routeTemplate: "{controller}/map",
                defaults: new { },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: "VersionedEntityMapApi",
                routeTemplate: "{version}/{controller}/map",
                defaults: new { },
                constraints: new { version = @"v\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: "EntityCrossMapApi",
                routeTemplate: "{controller}/crossmap",
                defaults: new { },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: "VersionedEntityCrossMapApi",
                routeTemplate: "{version}/{controller}/crossmap",
                defaults: new { },
                constraints: new { version = @"v\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: "EntitySearchApi",
                routeTemplate: "{controller}/search",
                defaults: new { },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get, HttpMethod.Post) }
            );

            config.Routes.MapHttpRoute(
                name: "VersionedEntitySearchApi",
                routeTemplate: "{version}/{controller}/search",
                defaults: new { },
                constraints: new { version = @"v\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Get, HttpMethod.Post) }
            );

            config.Routes.MapHttpRoute(
                name: "EntityFeedApi",
                routeTemplate: "{controller}/feed",
                defaults: new { },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: "VersionedEntityFeedApi",
                routeTemplate: "{version}/{controller}/feed",
                defaults: new { },
                constraints: new { version = @"v\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            // Register controller selector which generically supports all entities
            config.Services.Replace(typeof(IHttpControllerSelector), new DynamicEntityControllerSelector(config));
        }
    }
}