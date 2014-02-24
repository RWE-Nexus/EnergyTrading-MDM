namespace EnergyTrading.MDM.MappingService.Configuration
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Practices.Unity;

    using EnergyTrading.Configuration;
    using EnergyTrading.MDM.Configuration;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading.MDM.MappingService.Feeds;
    using EnergyTrading.Web;

    public class ServiceConfiguration : IGlobalConfigurationTask
    {
        private readonly IUnityContainer container;

        public ServiceConfiguration(IUnityContainer container)
        {
            this.container = container;
        }

        public IList<Type> DependsOn
        {
            get
            {
                return new List<Type> { typeof(MappingContextConfiguration) };
            }
        }

        public void Configure()
        {
            // Web layer
            this.container.RegisterType<IWebOperationContextWrapper, WebOperationContextWrapper>();

            // Service layer
            this.container.RegisterType<IFeedFactory, FeedFactory>();

            this.container.RegisterType<IConfigurationManager, AppConfigConfigurationManager>(new ContainerControlledLifetimeManager());
            var cacheRegistrar = new CacheRegistrar(this.container);
            CacheRegistrar.Instance.RegisterCache();
        }
    }
}