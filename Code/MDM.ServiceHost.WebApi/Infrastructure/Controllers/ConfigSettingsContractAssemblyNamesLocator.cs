using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace MDM.ServiceHost.WebApi.Infrastructure.Controllers
{
    public class ConfigSettingsContractAssemblyNamesLocator : IContractAssemblyNamesLocator
    {
        public const string ContractAssembliesKey = "ContractAssemblies";

        private readonly IList<string> contractAssemblyNames;

        public ConfigSettingsContractAssemblyNamesLocator()
        {
            Debug.Assert(ConfigurationManager.AppSettings[ContractAssembliesKey] != null,
                string.Format("Expect '{0}' key in configuration settings", ContractAssembliesKey));

            var assemblyNames = ConfigurationManager.AppSettings[ContractAssembliesKey].Split(';');

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