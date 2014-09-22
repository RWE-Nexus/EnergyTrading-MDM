using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EnergyTrading.Container.Unity.AutoRegistration;
using EnergyTrading.Mdm.Contracts;

namespace MDM.ServiceHost.WebApi.Infrastructure.Controllers
{
    public class MdmContractTypesLoader
    {
        private readonly IContractAssemblyNamesLocator contractAssemblyNamesLocator;

        public static IEnumerable<Type> ContractTypes { get; private set; }

        public static IEnumerable<Type> ContractListTypes { get; private set; }

        public MdmContractTypesLoader(IContractAssemblyNamesLocator contractAssemblyNamesLocator)
        {
            this.contractAssemblyNamesLocator = contractAssemblyNamesLocator;
        }

        public void LoadContractTypes()
        {
            ContractTypes =
                contractAssemblyNamesLocator.ContractAssemblyNames
                    .SelectMany(GetContractTypesFromAssembly)
                    .ToList();

            ContractListTypes =
                contractAssemblyNamesLocator.ContractAssemblyNames
                    .SelectMany(GetContractListTypesFromAssembly)
                    .ToList();
        }

        private IEnumerable<Type> GetContractTypesFromAssembly(string assemblyPath)
        {
            var assembly = Assembly.Load(new AssemblyName(assemblyPath));
            var allTypes = assembly.GetTypes();
            return allTypes.Where(x => x.Implements<IMdmEntity>()).ToList();
        }

        private IEnumerable<Type> GetContractListTypesFromAssembly(string assemblyPath)
        {
            var assembly = Assembly.Load(new AssemblyName(assemblyPath));
            var allTypes = assembly.GetTypes();
            return allTypes.Where(x => x.Name.EndsWith("List")).ToList(); // TODO: verify convention is sound
        }
    }
}