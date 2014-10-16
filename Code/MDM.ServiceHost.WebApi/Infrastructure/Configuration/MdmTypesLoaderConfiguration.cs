using System;
using System.Collections.Generic;
using EnergyTrading.Configuration;
using Microsoft.Practices.Unity;

namespace MDM.ServiceHost.WebApi.Infrastructure.Configuration
{
    public class MdmTypesLoaderConfiguration : IGlobalConfigurationTask
    {
        private readonly IUnityContainer container;

        public MdmTypesLoaderConfiguration(IUnityContainer container)
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
            var con = container.Resolve<MdmContractTypesLoader>();
            con.LoadContractTypes();

            var entityLoader = container.Resolve<MdmEntityTypesLoader>();
            entityLoader.LoadEntityTypes();

        }
    }
}