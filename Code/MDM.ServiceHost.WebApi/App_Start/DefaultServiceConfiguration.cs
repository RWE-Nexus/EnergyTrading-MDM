using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using EnergyTrading.Configuration;
using EnergyTrading.Mdm.ServiceHost.Unity.Configuration;
using EnergyTrading.Web;
using MDM.ServiceHost.WebApi.Filters;
using Microsoft.Practices.Unity;

namespace MDM.ServiceHost.WebApi
{
    /// <summary>
    /// Configures default services
    /// </summary>
    public static class DefaultServiceConfiguration
    {
        public static void Register(HttpConfiguration config, IUnityContainer container)
        {
            // Register error handling attribute for all Web API methods
            config.Filters.Add(new ErrorHandlingAttribute());

            // Replace the global exception handler with our own
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            // Add the default exception logger
            config.Services.Add(typeof(IExceptionLogger), new DefaultExceptionLogger());

            container.RegisterType<IWebOperationContextWrapper, WebOperationContextWrapper>();

            container.RegisterType<IConfigurationManager, AppConfigConfigurationManager>(new ContainerControlledLifetimeManager());
            
            var cacheRegistrar = new CacheRegistrar(container);
            cacheRegistrar.RegisterCache();
        }
    }
}