namespace MDM.AdcGateway.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    using MDM.Loader.NexusClient;

    using Microsoft.Practices.Unity;

    using RWEST.Nexus.Configuration;
    using RWEST.Nexus.MDM.Client.Services;
    using RWEST.Nexus.MDM.Client.WebClient;
    using RWEST.Nexus.MDM.Contracts;

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
                return new List<Type> { };
            }
        }

        public void Configure()
        {
            // Logging
            this.container.RegisterType<ILogger, DebugLogger>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<ILogger>("formLogger")));

            // Loader MDM client
            this.container.RegisterType<IMdmClient, MdmClient>();

            this.container.RegisterType<IPartyCrossMapService, PartyCrossMapService>();
        }
    }
}
