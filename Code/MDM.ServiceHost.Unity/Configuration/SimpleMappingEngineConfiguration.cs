namespace EnergyTrading.Mdm.ServiceHost.Unity.Configuration
{
    using System;
    using System.Collections.Generic;

    using EnergyTrading.Configuration;
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts.Mappers;
    using EnergyTrading.Mdm.Mappers;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    public class SimpleMappingEngineConfiguration : IGlobalConfigurationTask
    {
        private readonly IUnityContainer container;

        public SimpleMappingEngineConfiguration(IUnityContainer container)
        {
            this.container = container;
        }

        public IList<Type> DependsOn
        {
            get
            {
                return new List<Type>();
            }
        }

        public void Configure()
        {
            var mappingEngine = new SimpleMappingEngine(this.container.Resolve<IServiceLocator>());

            // Register in the container for consumers););
            this.container.RegisterInstance(typeof(IMappingEngine), mappingEngine);

            // Contract -> Domain

            // Domain -> Contract
            mappingEngine.RegisterMap(new SystemDataMapper());
            mappingEngine.RegisterMap(new EntityMappingMapper());
        }
    }
}