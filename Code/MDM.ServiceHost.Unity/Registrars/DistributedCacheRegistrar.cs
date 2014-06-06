namespace EnergyTrading.Mdm.ServiceHost.Unity.Registrars
{
    using System;

    using EnergyTrading.Caching;
    using EnergyTrading.Caching.AppFabric.Search;
    using EnergyTrading.Configuration;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm;
    using EnergyTrading.Mdm.ServiceHost.Unity.Configuration;
    using EnergyTrading.Mdm.Services;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    using Microsoft.ApplicationServer.Caching;
    using Microsoft.Practices.Unity;

    public class DistributedCacheRegistrar : ICacheRegistrar
    {
        private readonly IUnityContainer container;
        private readonly string configuredCacheName;

        public DistributedCacheRegistrar(IUnityContainer container)
        {
            if (container == null) { throw new ArgumentNullException("container"); }
            this.container = container;
            var configurationManager = container.Resolve<IConfigurationManager>();
            this.configuredCacheName = configurationManager.AppSettings["DistributedCacheName"] ?? "NexusMdmSearchCache";
        }

        public void RegisterCache()
        {
            var factory = new DataCacheFactory();
            var dataCache = factory.GetCache(this.configuredCacheName);
 
            this.container.RegisterInstance(this.configuredCacheName, dataCache);
            this.container.RegisterAbsoluteCacheItemPolicyFactory(this.configuredCacheName);
        }

        public void RegisterCachedMdmService<TContract, TEntity, TMdmService>(string entityName) where TContract : class where TEntity : class, IIdentifiable, IEntity where TMdmService : IMdmService<TContract, TEntity>
        {
            this.container.RegisterType<ISearchCache, RegionedAppFabricSearchCache>(
                entityName,
                new PerResolveLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<DataCache>(this.configuredCacheName),
                    new ResolvedParameter<ICacheItemPolicyFactory>(this.configuredCacheName),
                    entityName));

            this.container.RegisterType<IMdmService<TContract, TEntity>, TMdmService>(
                new InjectionConstructor(
                    new ResolvedParameter<IValidatorEngine>(entityName),
                    new ResolvedParameter<IMappingEngine>(),
                    new ResolvedParameter<IRepository>(),
                    new ResolvedParameter<ISearchCache>(entityName)));
        }
    }
}