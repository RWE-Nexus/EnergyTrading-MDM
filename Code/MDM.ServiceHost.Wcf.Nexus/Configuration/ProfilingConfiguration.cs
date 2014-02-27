namespace EnergyTrading.MDM.MappingService.Configuration
{
    using System;
    using System.Collections.Generic;

    using HibernatingRhinos.Profiler.Appender.EntityFramework;

    using EnergyTrading.Configuration;

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