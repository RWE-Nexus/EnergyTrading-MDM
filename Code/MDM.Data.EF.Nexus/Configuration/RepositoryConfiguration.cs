namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System;
    using System.Collections.Generic;

    using EnergyTrading.Configuration;
    using EnergyTrading.Data;
    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Actions;

    using Microsoft.Practices.Unity;

    public class RepositoryConfiguration : IGlobalConfigurationTask
    {
        private readonly IUnityContainer container;

        public RepositoryConfiguration(IUnityContainer container)
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
            // Database stuff
            this.container.RegisterType<IRepository, DbSetRepository>();
//            this.container.RegisterType(typeof(IRepository<>), typeof(DbSetRepository<>));

            this.container.RegisterInstance(
                typeof(IList<Action<IDbSetRepository>>),
                new List<Action<IDbSetRepository>> { CalendarActions.CascadeCalendarDay });

            this.container.RegisterInstance(
                typeof(IList<Action<IDao>>),
                new List<Action<IDao>> { ContextInfoActions.SetContextUserInfo });
        }
    }
}