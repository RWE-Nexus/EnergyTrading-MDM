using System.Web.Http;
using System.Web.Http.Dispatcher;
using EnergyTrading.MDM.MappingService2.Filters;
using EnergyTrading.MDM.MappingService2.Infrastructure.Controllers;

namespace EnergyTrading.MDM.MappingService2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "MappingApi",
                routeTemplate: "{controller}/{id}/mapping/{mappingid}",
                defaults: new { mappingid = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "MappingsApi",
                routeTemplate: "{controller}/{id}/mappings"
            );

            config.Routes.MapHttpRoute(
                name: "ListApi",
                routeTemplate: "{controller}/list"
            );

            config.Routes.MapHttpRoute(
                name: "EntityListApi",
                routeTemplate: "{controller}/{id}/list"
            );

            config.Routes.MapHttpRoute(
                name: "EntityMapApi",
                routeTemplate: "{controller}/map"
            );

            config.Routes.MapHttpRoute(
                name: "EntityCrossMapApi",
                routeTemplate: "{controller}/crossmap"
            );

            config.Routes.MapHttpRoute(
                name: "EntitySearchApi",
                routeTemplate: "{controller}/search"
            );

            config.Routes.MapHttpRoute(
                name: "EntityFeedApi",
                routeTemplate: "{controller}/feed"
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();

            // Register controller selector which generically supports all entities
            config.Services.Replace(typeof(IHttpControllerSelector), new DynamicEntityControllerSelector(config));

            // Register error handling and logging attributes for all Web API methods
            config.Filters.Add(new ErrorHandlingAttribute());
            config.Filters.Add(new LoggingAttribute());
        }
    }
}
