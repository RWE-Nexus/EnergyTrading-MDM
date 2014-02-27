namespace EnergyTrading.MDM.Configuration
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading.Configuration;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Mappers;

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
            var mappingEngine = new SimpleMappingEngine(container.Resolve<IServiceLocator>());

            // Register in the container for consumers););
            this.container.RegisterInstance(typeof(IMappingEngine), mappingEngine);

            // Contract -> Domain

            // Domain -> Contract
            mappingEngine.RegisterMap(new SystemDataMapper());
            mappingEngine.RegisterMap(new EntityMappingMapper());
        }
    }
}