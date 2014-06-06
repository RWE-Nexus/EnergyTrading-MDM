namespace MDM.ServiceHost.WebApi.Infrastructure.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;

    using EnergyTrading.Mdm;

    using MDM.ServiceHost.WebApi.Controllers;

    using EnergyTrading.Mdm.Contracts;

    public class DynamicEntityControllerSelector : DefaultHttpControllerSelector
    {
        private readonly HttpConfiguration configuration;
        private readonly IEnumerable<Type> contractTypes;
        private readonly IEnumerable<Type> entityTypes;
        private IEnumerable<Type> listContractTypes;

        public DynamicEntityControllerSelector(HttpConfiguration configuration) : base(configuration)
        {
            // Get types in MDM.Core that implement MdmService and cache them?

            var coreAssembly = Assembly.Load(new AssemblyName("MDM.Core.Nexus"));
            var contractAssembly = Assembly.Load(new AssemblyName("EnergyTrading.Mdm.Contracts"));

            this.contractTypes = contractAssembly.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IMdmEntity)));
            this.entityTypes = coreAssembly.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IEntity)));
            this.listContractTypes = contractAssembly.GetTypes(); // Too broad a net to cast?

            this.configuration = configuration;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var entityName = this.GetControllerName(request);

            var contractType = this.contractTypes.First(x => x.Name.Equals(entityName, StringComparison.InvariantCultureIgnoreCase));
            var entityType = this.entityTypes.First(x => x.Name.Equals(entityName, StringComparison.InvariantCultureIgnoreCase));
            var listContractType = this.listContractTypes.First(x => x.Name.Equals(entityName + "List", StringComparison.InvariantCultureIgnoreCase));

            var controllerType = GetControllerTypeForRequest(request, contractType, entityType, listContractType);

            return new HttpControllerDescriptor(this.configuration, "EntityController`2", controllerType);
        }

        private static Type GetControllerTypeForRequest(HttpRequestMessage request, Type contractType, Type entityType, Type listContractType)
        {
            // HACK, and will need fixing to prevent false detection, e.g. if an Entity name contains "list" or "map"
            if (request.RequestUri.AbsolutePath.Contains("mappings", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(EntityMappingsController<,>).MakeGenericType(new[] { contractType, entityType });
            }
            
            if (request.RequestUri.AbsolutePath.Contains("mapping", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(EntityMappingController<,>).MakeGenericType(new[] { contractType, entityType });
            }

            if (request.RequestUri.AbsolutePath.Contains(contractType.Name + "/list", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(EntityListController<,,>).MakeGenericType(new[] { contractType, entityType, listContractType });
            }
            
            if (request.RequestUri.AbsolutePath.Contains("list", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(EntityEntityListController<,>).MakeGenericType(new[] { contractType, entityType });
            }

            if (request.RequestUri.AbsolutePath.Contains("crossmap", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(EntityCrossMapController<,>).MakeGenericType(new[] { contractType, entityType });
            }

            if (request.RequestUri.AbsolutePath.Contains("map", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(EntityMapController<,>).MakeGenericType(new[] { contractType, entityType });
            }

            if (request.RequestUri.AbsolutePath.Contains("search", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(EntitySearchController<,>).MakeGenericType(new[] { contractType, entityType });
            }

            if (request.RequestUri.AbsolutePath.Contains("feed", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(EntityFeedController<,>).MakeGenericType(new[] { contractType, entityType });
            }

            return typeof(EntityController<,>).MakeGenericType(new[] { contractType, entityType });
        }
    }
}