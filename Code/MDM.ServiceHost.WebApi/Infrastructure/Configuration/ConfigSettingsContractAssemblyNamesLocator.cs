using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using EnergyTrading.Configuration;

namespace MDM.ServiceHost.WebApi.Infrastructure.Configuration
{
    public class ConfigSettingsContractAssemblyNamesLocator : IContractAssemblyNamesLocator
    {
        public const string ContractAssembliesKey = "ContractAssemblies";

        private readonly IList<string> contractAssemblyNames;

        public ConfigSettingsContractAssemblyNamesLocator(IConfigurationManager configurationManager)
        {
            if (configurationManager == null)
            {
                throw new ArgumentNullException("configurationManager");
            }

            if (configurationManager.AppSettings[ContractAssembliesKey] == null)
            {
                throw new ConfigurationErrorsException(string.Format("Contract assemblies must be configured against the '{0}' key in configuration settings", ContractAssembliesKey));
            }

            var assemblyNames = configurationManager.AppSettings[ContractAssembliesKey].Split(';');

            contractAssemblyNames = assemblyNames.Select(x => x.Trim()).ToList();
        }

        public IEnumerable<string> ContractAssemblyNames
        {
            get
            {
                return contractAssemblyNames;
            }
        }
    }
}