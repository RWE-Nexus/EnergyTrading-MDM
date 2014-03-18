namespace EnergyTrading.MDM.ServiceHost.Wcf.Nexus.Configuration
{
    using System;
    using System.Collections.Generic;

    using EnergyTrading.Configuration;
    using EnergyTrading.Logging;
    using EnergyTrading.Logging.EnterpriseLibrary;

    using Microsoft.Practices.Unity;

    public class LoggerConfiguration : IGlobalConfigurationTask
    {
        private readonly IUnityContainer container;

        public LoggerConfiguration(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            
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
            var loggerFactory = new EntLibLoggerFactory();

            LoggerFactory.SetProvider(() => loggerFactory);
            LoggerFactory.Initialize();
        }
    }
}