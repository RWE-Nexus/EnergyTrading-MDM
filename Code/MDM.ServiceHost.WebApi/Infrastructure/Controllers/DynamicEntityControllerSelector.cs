﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using MDM.ServiceHost.WebApi.Controllers;

namespace MDM.ServiceHost.WebApi.Infrastructure.Controllers
{
    /// <summary>
    /// This is the service entry point controller where it dynamically determines from the request url which 
    /// MDM entity is being requested and which type of action and then returns the details of the particular
    /// controller that should be invoked.
    /// </summary>
    public class DynamicEntityControllerSelector : DefaultHttpControllerSelector
    {
        private readonly HttpConfiguration configuration;
        private readonly IEnumerable<Type> contractTypes;
        private readonly IEnumerable<Type> entityTypes;
        private readonly IEnumerable<Type> listContractTypes;

        public DynamicEntityControllerSelector(HttpConfiguration configuration)
            : base(configuration)
        {
            contractTypes = MdmContractTypesLoader.ContractTypes;
            entityTypes = MdmEntityTypesLoader.EntityTypes;
            listContractTypes = MdmContractTypesLoader.ContractListTypes;

            this.configuration = configuration;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var contractName = GetControllerName(request);

            var entityName = DetermineVersionedEntityName(request, contractName);

            var contractType = DetermineContractType(contractName);

            var entityType = DetermineEntityType(entityName);

            var listContractType = DetermineListContractType(contractName);

            var controllerType = GetControllerTypeForRequest(request, contractType, entityType, listContractType);

            return new HttpControllerDescriptor(configuration, "EntityController`2", controllerType);
        }

        private Type DetermineListContractType(string contractName)
        {
            var listContractType = listContractTypes.FirstOrDefault(x => x.Name.Equals(contractName + "List", StringComparison.InvariantCultureIgnoreCase));
            if (listContractType == null)
            {
                throw new Exception(string.Format("Unknown MDM resource list type: {0}List", contractName));
            }
            return listContractType;
        }

        private Type DetermineEntityType(string entityName)
        {
            var entityType = entityTypes.FirstOrDefault(x => x.Name.Equals(entityName, StringComparison.InvariantCultureIgnoreCase));
            if (entityType == null)
            {
                throw new Exception(
                    string.Format("The MDM service has determined an unknown service entity from the request: {0}", entityName));
            }
            return entityType;
        }

        private Type DetermineContractType(string contractName)
        {
            var contractType = contractTypes.FirstOrDefault(x => x.Name.Equals(contractName, StringComparison.InvariantCultureIgnoreCase));
            if (contractType == null)
            {
                throw new Exception(string.Format(
                    "The MDM service has determined an unknown MDM resource from the request: {0}", contractName));
            }
            return contractType;
        }

        private static string DetermineVersionedEntityName(HttpRequestMessage request, string contractName)
        {
            var uri = request.RequestUri.AbsolutePath.ToLowerInvariant();
            var match = Regex.Match(uri, "/v[\\d]+/");
            if (match.Success && match.Value != "/v1/")
            {
                return string.Format("{0}{1}", contractName,
                    match.Value.Substring(1, match.Value.Length - 2).ToUpperInvariant());
            }
            return contractName;
        }

        private static Type GetControllerTypeForRequest(HttpRequestMessage request, Type contractType, Type entityType, Type listContractType)
        {
            // TODO: make more solid to prevent false detection
            if (request.RequestUri.AbsolutePath.Contains("/mappings", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(EntityMappingsController<,>).MakeGenericType(new[] { contractType, entityType });
            }

            if (request.RequestUri.AbsolutePath.Contains("/mapping", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(EntityMappingController<,>).MakeGenericType(new[] { contractType, entityType });
            }

            if (request.RequestUri.AbsolutePath.Contains(contractType.Name + "/list", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(EntityListController<,,>).MakeGenericType(new[] { contractType, entityType, listContractType });
            }

            if (request.RequestUri.AbsolutePath.Contains("/list", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(EntityEntityListController<,>).MakeGenericType(new[] { contractType, entityType });
            }

            if (request.RequestUri.AbsolutePath.Contains("/crossmap", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(EntityCrossMapController<,>).MakeGenericType(new[] { contractType, entityType });
            }

            if (request.RequestUri.AbsolutePath.Contains("/map", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(EntityMapController<,>).MakeGenericType(new[] { contractType, entityType });
            }

            if (request.RequestUri.AbsolutePath.Contains("/search", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(EntitySearchController<,>).MakeGenericType(new[] { contractType, entityType });
            }

            if (request.RequestUri.AbsolutePath.Contains("/feed", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(EntityFeedController<,>).MakeGenericType(new[] { contractType, entityType });
            }

            return typeof(EntityController<,>).MakeGenericType(new[] { contractType, entityType });
        }
    }
}