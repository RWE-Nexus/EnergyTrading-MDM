using System.Configuration;
using System.Diagnostics;
using EnergyTrading.Configuration;
using EnergyTrading.Container.Unity.AutoRegistration;
using Microsoft.Practices.Unity;

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

        public DynamicEntityControllerSelector(HttpConfiguration configuration)
            : base(configuration)
        {
            // Get types in MDM.Core that implement MdmService and cache them?
            this.contractTypes = MdmContractTypesLoader.ContractTypes;
            this.entityTypes = MdmEntityTypesLoader.EntityTypes;
            this.listContractTypes = MdmContractTypesLoader.ContractListTypes;

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
            // TODO: HACK, and will need fixing to prevent false detection, e.g. if an Entity name contains "list" or "map"
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

    public interface IContractAssemblyNamesLocator
    {
        IEnumerable<string> ContractAssemblyNames { get; }
    }

    public interface IEntityAssemblyNamesLocator
    {
        IEnumerable<string> EntityAssemblyNames { get; }
    }

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

    public class MdmTypesLoderConfiguration : IGlobalConfigurationTask
    {
        private readonly IUnityContainer container;

        public MdmTypesLoderConfiguration(IUnityContainer container)
        {
            this.container = container;
        }

        public IList<Type> DependsOn
        {
            get
            {
                return new List<Type>();
            }
        }

        public void Configure()
        {
            container.RegisterType<IContractAssemblyNamesLocator, ConfigSettingsContractAssemblyNamesLocator>();
            container.RegisterType<IEntityAssemblyNamesLocator, ConfigSettingsEntityAssemblyNamesLocator>();

            var con = container.Resolve<MdmContractTypesLoader>();
            con.LoadContractTypes();

            var entityLoader = container.Resolve<MdmEntityTypesLoader>();
            entityLoader.LoadEntityTypes();

        }
    }

}