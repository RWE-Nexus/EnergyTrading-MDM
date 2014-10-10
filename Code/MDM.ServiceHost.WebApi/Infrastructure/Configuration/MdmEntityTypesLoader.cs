using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EnergyTrading.Container.Unity.AutoRegistration;
using EnergyTrading.Mdm;

namespace MDM.ServiceHost.WebApi.Infrastructure.Configuration
{
    public class MdmEntityTypesLoader
    {
        private readonly IEntityAssemblyNamesLocator entityAssemblyNamesLocator;
        public static IEnumerable<Type> EntityTypes { get; private set; }

        public MdmEntityTypesLoader(IEntityAssemblyNamesLocator entityAssemblyNamesLocator)
        {
            this.entityAssemblyNamesLocator = entityAssemblyNamesLocator;
        }

        public void LoadEntityTypes()
        {
            EntityTypes =
                entityAssemblyNamesLocator.EntityAssemblyNames
                    .SelectMany(GetEntityTypesFromAssembly)
                    .ToList();
        }

        private IEnumerable<Type> GetEntityTypesFromAssembly(string assemblyPath)
        {
            var assembly = Assembly.Load(new AssemblyName(assemblyPath));
            var allTypes = assembly.GetTypes();
            return allTypes.Where(x => x.Implements<IEntity>()).ToList();
        }
    }
}