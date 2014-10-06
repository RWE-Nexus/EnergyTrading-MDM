using System.Web.Http;
using System.Web.Http.Dispatcher;
using MDM.ServiceHost.WebApi.Filters;
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
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}"
            );

            config.Routes.MapHttpRoute(
                name: "VersionedDefaultApi",
                routeTemplate: "{version}/{controller}",
                defaults: new { },
                constraints: new { version = @"v\d+" }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultIdApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "VersionedDefaultIdApi",
                routeTemplate: "{version}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { version = @"v\d+" }
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
                routeTemplate: "{controller}/{id}/mappings"
            );

            config.Routes.MapHttpRoute(
                name: "VersionedMappingsApi",
                routeTemplate: "{version}/{controller}/{id}/mappings",
                defaults: new { },
                constraints: new { version = @"v\d+" }
            );

            config.Routes.MapHttpRoute(
                name: "ListApi",
                routeTemplate: "{controller}/list"
            );

            config.Routes.MapHttpRoute(
                name: "VersionedListApi",
                routeTemplate: "{version}/{controller}/list",
                defaults: new { },
                constraints: new { version = @"v\d+" }
            );

            config.Routes.MapHttpRoute(
                name: "EntityListApi",
                routeTemplate: "{controller}/{id}/list"
            );

            config.Routes.MapHttpRoute(
                name: "VersionedEntityListApi",
                routeTemplate: "{version}/{controller}/{id}/list",
                defaults: new { },
                constraints: new { version = @"v\d+" }
            );

            config.Routes.MapHttpRoute(
                name: "EntityMapApi",
                routeTemplate: "{controller}/map"
            );

            config.Routes.MapHttpRoute(
                name: "VersionedEntityMapApi",
                routeTemplate: "{version}/{controller}/map",
                defaults: new { },
                constraints: new { version = @"v\d+" }
            );

            config.Routes.MapHttpRoute(
                name: "EntityCrossMapApi",
                routeTemplate: "{controller}/crossmap"
            );

            config.Routes.MapHttpRoute(
                name: "VersionedEntityCrossMapApi",
                routeTemplate: "{version}/{controller}/crossmap",
                defaults: new { },
                constraints: new { version = @"v\d+" }
            );

            config.Routes.MapHttpRoute(
                name: "EntitySearchApi",
                routeTemplate: "{controller}/search"
            );

            config.Routes.MapHttpRoute(
                name: "VersionedEntitySearchApi",
                routeTemplate: "{version}/{controller}/search",
                defaults: new { },
                constraints: new { version = @"v\d+" }
            );

            config.Routes.MapHttpRoute(
                name: "EntityFeedApi",
                routeTemplate: "{controller}/feed"
            );

            config.Routes.MapHttpRoute(
                name: "VersionedEntityFeedApi",
                routeTemplate: "{version}/{controller}/feed",
                defaults: new { },
                constraints: new { version = @"v\d+" }
            );

            // Register controller selector which generically supports all entities
            config.Services.Replace(typeof(IHttpControllerSelector), new DynamicEntityControllerSelector(config));

            // Register error handling attribute for all Web API methods
            config.Filters.Add(new ErrorHandlingAttribute());
        }
    }
}