namespace EnergyTrading.MDM.ServiceHost.Wcf.Nexus.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using AutoMapper;

    using EnergyTrading.Configuration;

    using Microsoft.Practices.Unity;

    public class AutoMapperConfiguration : IGlobalConfigurationTask
    {
        private readonly IUnityContainer container;

        public AutoMapperConfiguration(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            
            this.container = container;
        }

        public void Configure()
        {
            InitialiseMappingEngine(this.container);
            this.container.RegisterInstance(Mapper.Engine);
        }

        public IList<Type> DependsOn
        {
            get
            {
                return new List<Type>();
            }
        }

        private static void InitialiseMappingEngine(IUnityContainer container)
        {
            Mapper.Reset();

            Mapper.Initialize(cfg =>
                {
                    var profileTypes = GetProfileTypes();
                    foreach (var profileType in profileTypes)
                    {
                        Mapper.Configuration.AddProfile(container.Resolve(profileType) as Profile);
                    }
                    cfg.ConstructServicesUsing(x => container.Resolve(x));
                });

//            Mapper.AssertConfigurationIsValid();
        }

        private static IEnumerable<Type> GetProfileTypes()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !type.IsAbstract && typeof(Profile).IsAssignableFrom(type));
        }
    }
}