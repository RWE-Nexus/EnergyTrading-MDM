namespace EnergyTrading.Mdm.ServiceHost.Unity.Configuration
{
    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Services;

    /// <summary>
    /// Register caches
    /// </summary>
    public interface ICacheRegistrar
    {
        /// <summary>
        /// Register the cache.
        /// </summary>
        void RegisterCache();

        /// <summary>
        /// Register the service cache.
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TMdmService"></typeparam>
        /// <param name="entityName"></param>
        void RegisterCachedMdmService<TContract, TEntity, TMdmService>(string entityName)
            where TMdmService : IMdmService<TContract, TEntity> 
            where TContract : class
            where TEntity : class, IIdentifiable, IEntity;
    }
}