using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace MDM.ServiceHost.WebApi.Infrastructure.Controllers
{
    public class ConfigSettingsEntityAssemblyNamesLocator : IEntityAssemblyNamesLocator
    {
        public const string EntityAssembliesKey = "EntityAssemblies";

        private readonly IList<string> entityAssemblyNames;

        public ConfigSettingsEntityAssemblyNamesLocator()
        {
            Debug.Assert(ConfigurationManager.AppSettings[EntityAssembliesKey] != null,
                string.Format("Expect '{0}' key in configuration settings", EntityAssembliesKey));

            var assemblyNames = ConfigurationManager.AppSettings[EntityAssembliesKey].Split(';');

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