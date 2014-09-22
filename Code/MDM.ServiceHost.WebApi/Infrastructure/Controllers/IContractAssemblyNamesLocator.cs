using System.Collections.Generic;

namespace MDM.ServiceHost.WebApi.Infrastructure.Controllers
{
    public interface IContractAssemblyNamesLocator
    {
        IEnumerable<string> ContractAssemblyNames { get; }
    }
}