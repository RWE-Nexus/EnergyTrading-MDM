using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using EnergyTrading.MDM.MappingService2.Controllers;
using RWEST.Nexus.MDM.Contracts;

namespace EnergyTrading.MDM.MappingService2.Infrastructure.Controllers
{
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
            var contractAssembly = Assembly.Load(new AssemblyName("EnergyTrading.MDM.Contracts"));

            contractTypes = contractAssembly.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IMdmEntity)));
            entityTypes = coreAssembly.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IEntity)));
            listContractTypes = contractAssembly.GetTypes(); // Too broad a net to cast?

            this.configuration = configuration;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var entityName = GetControllerName(request);

            var contractType = contractTypes.First(x => x.Name.Equals(entityName, StringComparison.InvariantCultureIgnoreCase));
            var entityType = entityTypes.First(x => x.Name.Equals(entityName, StringComparison.InvariantCultureIgnoreCase));
            var listContractType = listContractTypes.First(x => x.Name.Equals(entityName + "List", StringComparison.InvariantCultureIgnoreCase));

            var controllerType = GetControllerTypeForRequest(request, contractType, entityType, listContractType);

            return new HttpControllerDescriptor(configuration, "EntityController`2", controllerType);
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