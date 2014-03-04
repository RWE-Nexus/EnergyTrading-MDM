namespace EnergyTrading.MDM.ServiceHost.Unity.Registrars
{
    using System;

    using EnergyTrading.Caching;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM;
    using EnergyTrading.MDM.ServiceHost.Unity.Configuration;
    using EnergyTrading.MDM.Services;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    using Microsoft.Practices.Unity;

    public class ObjectCacheRegistrar : ICacheRegistrar
    {
        private readonly IUnityContainer container;

        private const string CacheKey = "Nexus.SearchCache";

        public ObjectCacheRegistrar(IUnityContainer container)
        {
            if (container == null) { throw new ArgumentNullException("container"); }
            this.container = container;
        }

        public void RegisterCache()
        {
            this.container.RegisterAbsoluteCacheItemPolicyFactory(CacheKey);

            this.container.RegisterType<ISearchCache, SearchCache>(
                new PerResolveLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<ICacheItemPolicyFactory>(CacheKey)));
        }

        public void RegisterCachedMdmService<TContract, TEntity, TMdmService>(string entityName) where TContract : class where TEntity : class, IIdentifiable, IEntity where TMdmService : IMdmService<TContract, TEntity>
        {
            this.container.RegisterType<IMdmService<TContract, TEntity>, TMdmService>(
                new InjectionConstructor(
                    new ResolvedParameter<IValidatorEngine>(entityName),
                    new ResolvedParameter<IMappingEngine>(),
                    new ResolvedParameter<IRepository>(),
                    new ResolvedParameter<ISearchCache>()));
        }
    }
}