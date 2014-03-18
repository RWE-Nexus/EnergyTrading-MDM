namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using EnergyTrading.Configuration;
    using EnergyTrading.Container.Unity;
    using EnergyTrading.Data.EntityFramework;

    using Microsoft.Practices.Unity;

    public class NexusMappingContextConfiguration : IGlobalConfigurationTask
    {
        private readonly IUnityContainer container;

        public NexusMappingContextConfiguration(IUnityContainer container)
        {
            this.container = container;
        }

        public IList<Type> DependsOn
        {
            get
            {
                return new List<Type> { };
            }
        }

        public void Configure()
        {
            // Stops EF from trying to modify the schema
            Database.SetInitializer(new NullDatabaseInitializer<NexusMappingContext>());

            this.container.RegisterInstance<Func<DbContext>>(() => new NexusMappingContext());
            this.container.RegisterType<IDbContextProvider, DbContextProvider>(CallContextLifetimeFactory.Manager());
        }
    }
}