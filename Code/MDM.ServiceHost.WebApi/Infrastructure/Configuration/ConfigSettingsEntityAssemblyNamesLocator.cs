using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using EnergyTrading.Configuration;

namespace MDM.ServiceHost.WebApi.Infrastructure.Configuration
{
    public class ConfigSettingsEntityAssemblyNamesLocator : IEntityAssemblyNamesLocator
    {
        public const string EntityAssembliesKey = "EntityAssemblies";

        private readonly IList<string> entityAssemblyNames;

        public ConfigSettingsEntityAssemblyNamesLocator(IConfigurationManager configurationManager)
        {
            if (configurationManager == null)
            {
                throw new ArgumentNullException("configurationManager");
            }

            if (configurationManager.AppSettings[EntityAssembliesKey] == null)
            {
                throw new ConfigurationErrorsException(string.Format("Entity assemblies must be configured against the '{0}' key in configuration settings", EntityAssembliesKey));
            }

            var assemblyNames = configurationManager.AppSettings[EntityAssembliesKey].Split(';');

            entityAssemblyNames = assemblyNames.Select(x => x.Trim()).ToList();
        }

        public IEnumerable<string> EntityAssemblyNames
        {
            get
            {
                return entityAssemblyNames;
            }
        }
    }
}