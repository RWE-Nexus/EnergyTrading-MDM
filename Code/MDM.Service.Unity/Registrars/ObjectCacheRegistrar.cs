namespace MDM.Service.Unity.Registrars
{
    using System;

    using EnergyTrading.MDM;
    using EnergyTrading.MDM.Configuration;

    using Microsoft.Practices.Unity;

    using EnergyTrading.Caching;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Services;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

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
            container.RegisterAbsoluteCacheItemPolicyFactory(CacheKey);

            container.RegisterType<ISearchCache, SearchCache>(
                new PerResolveLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<ICacheItemPolicyFactory>(CacheKey)));
        }

        public void RegisterCachedMdmService<TContract, TEntity, TMdmService>(string entityName) where TContract : class where TEntity : class, IIdentifiable, IEntity where TMdmService : IMdmService<TContract, TEntity>
        {
            container.RegisterType<IMdmService<TContract, TEntity>, TMdmService>(
                new InjectionConstructor(
                    new ResolvedParameter<IValidatorEngine>(entityName),
                    new ResolvedParameter<IMappingEngine>(),
                    new ResolvedParameter<IRepository>(),
                    new ResolvedParameter<ISearchCache>()));
        }
    }
}