using System.Collections.Generic;

namespace MDM.ServiceHost.WebApi.Infrastructure.Configuration
{
    public interface IContractAssemblyNamesLocator
    {
        IEnumerable<string> ContractAssemblyNames { get; }
    }
}