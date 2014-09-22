using System.Collections.Generic;

namespace MDM.ServiceHost.WebApi.Infrastructure.Controllers
{
    public interface IEntityAssemblyNamesLocator
    {
        IEnumerable<string> EntityAssemblyNames { get; }
    }
}