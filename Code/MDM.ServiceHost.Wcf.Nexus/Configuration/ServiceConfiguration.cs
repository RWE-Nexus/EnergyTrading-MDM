namespace EnergyTrading.MDM.ServiceHost.Wcf.Nexus.Configuration
{
    using System;
    using System.Collections.Generic;

    using EnergyTrading.Configuration;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading.MDM.ServiceHost.Unity.Configuration;
    using EnergyTrading.Web;

    using global::MDM.ServiceHost.Wcf.Feeds;

    using Microsoft.Practices.Unity;

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
                return new List<Type> { typeof(NexusMappingContextConfiguration) };
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