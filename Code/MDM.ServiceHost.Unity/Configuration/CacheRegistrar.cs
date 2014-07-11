namespace EnergyTrading.Mdm.ServiceHost.Unity.Configuration
{
    using System;
    using System.Configuration;

    using EnergyTrading.Mdm.ServiceHost.Unity.Registrars;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// Determines which <see cref="ICacheRegistrar "/> to use.
    /// </summary>
    public class CacheRegistrar : ICacheRegistrar
    {
        private readonly IUnityContainer container;
        private ICacheRegistrar instance;

        /// <summary>
        /// Creates a new instance of the <see cref="CacheRegistrar"/> class.
        /// </summary>
        /// <param name="container"></param>
        public CacheRegistrar(IUnityContainer container)
        {
            if (container == null) { throw new ArgumentNullException("container"); }
            this.container = container;
        }

        private ICacheRegistrar Instance
        {
            get {return instance ?? (instance = Create()); }
        }

        private ICacheRegistrar Create()
        {
            var isDistributedValue = ConfigurationManager.AppSettings["UseDistributedCache"];
            bool isDistributed;
            if (bool.TryParse(isDistributedValue, out isDistributed) && isDistributed)
            {
                return new DistributedCacheRegistrar(container);
            }            
            return new ObjectCacheRegistrar(container);
        }

        public void RegisterCache()
        {
            Instance.RegisterCache();
        }

        public void RegisterCachedMdmService<TContract, TEntity, TMdmService>(string entityName)
            where TContract : class
            where TEntity : class, EnergyTrading.Data.IIdentifiable, IEntity
            where TMdmService : Services.IMdmService<TContract, TEntity>
        {
            Instance.RegisterCachedMdmService<TContract, TEntity, TMdmService>(entityName);
        }
    }
}