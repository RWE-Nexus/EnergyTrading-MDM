namespace EnergyTrading.Mdm.ServiceHost.Unity.Configuration
{
    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Services;

    public interface ICacheRegistrar
    {
        void RegisterCache();

        void RegisterCachedMdmService<TContract, TEntity, TMdmService>(string entityName)
            where TMdmService : IMdmService<TContract, TEntity> 
            where TContract : class
            where TEntity : class, IIdentifiable, IEntity;
    }
}