﻿namespace EnergyTrading.Mdm.ServiceHost.Unity.Configuration
{
    using System;
    using System.Configuration;

    using EnergyTrading.Mdm.ServiceHost.Unity.Registrars;

    using Microsoft.Practices.Unity;

    public class CacheRegistrar
    {
        private static IUnityContainer container;

        private static ICacheRegistrar instance;

        public CacheRegistrar(IUnityContainer theContainer)
        {
            if (theContainer == null) { throw new ArgumentNullException("theContainer"); }
            container = theContainer;
        }

        public static ICacheRegistrar Instance
        {
            get
            {
                return instance ?? (instance = ConstructInstance());
            }
        }

        private static ICacheRegistrar ConstructInstance()
        {
            var isDistributedValue = ConfigurationManager.AppSettings["UseDistributedCache"];
            bool isDistributed;
            if (bool.TryParse(isDistributedValue, out isDistributed) && isDistributed)
            {
                return new DistributedCacheRegistrar(container);
            }            
            return new ObjectCacheRegistrar(container);
        }
    }
}