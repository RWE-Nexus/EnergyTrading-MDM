using System.Collections.Generic;

namespace MDM.ServiceHost.WebApi.Infrastructure.Configuration
{
    public interface IEntityAssemblyNamesLocator
    {
        IEnumerable<string> EntityAssemblyNames { get; }
    }
}