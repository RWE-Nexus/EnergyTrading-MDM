namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using EnergyTrading.Container.Unity;

    using Microsoft.Practices.Unity;

    using EnergyTrading.Configuration;
    using EnergyTrading.Data.EntityFramework;

    public class MappingContextConfiguration : IGlobalConfigurationTask
    {
        private readonly IUnityContainer container;

        public MappingContextConfiguration(IUnityContainer container)
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
            Database.SetInitializer(new NullDatabaseInitializer<MappingContext>());

            this.container.RegisterInstance<Func<DbContext>>(() => new MappingContext());
            this.container.RegisterType<IDbContextProvider, DbContextProvider>(CallContextLifetimeFactory.Manager());
        }
    }
}