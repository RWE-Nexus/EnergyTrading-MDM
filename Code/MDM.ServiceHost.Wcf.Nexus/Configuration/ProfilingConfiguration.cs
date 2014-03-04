namespace EnergyTrading.MDM.ServiceHost.Wcf.Nexus.Configuration
{
    using System;
    using System.Collections.Generic;

    using EnergyTrading.Configuration;

    using HibernatingRhinos.Profiler.Appender.EntityFramework;

    public class ProfilingConfiguration : IGlobalConfigurationTask
    {
        public IList<Type> DependsOn
        {
            get { return new List<Type>(); }
        }

        public void Configure()
        {
            EntityFrameworkProfiler.Initialize();
        }
    }
}