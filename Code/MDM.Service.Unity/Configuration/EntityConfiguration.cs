namespace EnergyTrading.MDM.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    using Microsoft.ApplicationServer.Caching;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    using global::EnergyTrading.Caching.AppFabric.Search;

    using EnergyTrading.Caching;
    using EnergyTrading.Configuration;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.MDM.Messages.Validators;
    using EnergyTrading.MDM.Services;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    public abstract class EntityConfiguration<TMdmService, TEntity, TContract, TMapping, TContractValidator> : IGlobalConfigurationTask
        where TMdmService : IMdmService<TContract, TEntity>
        where TContract : class
        where TEntity : class, IIdentifiable, IEntity
        where TMapping : class, IEntityMapping
        where TContractValidator : IValidator<TContract>
    {
        private IMappingEngine mappingEngine;

        protected EntityConfiguration(IUnityContainer container)
        {
            this.Container = container;
        }

        protected abstract string Name { get; }

        public virtual IList<Type> DependsOn
        {
            get { return new List<Type> { typeof(SimpleMappingEngineConfiguration) }; }
        }

        protected IUnityContainer Container { get; private set; }

        protected IMappingEngine MappingEngine
        {
            get
            {
                if (mappingEngine == null)
                {
                    this.mappingEngine = Container.Resolve<IMappingEngine>();
                }
                return mappingEngine;
            }
            set { mappingEngine = value; }
        }

        public void Configure()
        {
            this.ConfigureServices();
            this.ConfigureValidation();
            this.ConfigureMapping();
            this.OnConfigure();
        }

        protected virtual void OnConfigure()
        {
        }

        protected void ConfigureServices()
        {
            // MDM layer
            var registrar = new CacheRegistrar(this.Container); // TODO this is awful
            CacheRegistrar.Instance.RegisterCachedMdmService<TContract, TEntity, TMdmService>(Name);
        }

        protected void ConfigureMapping()
        {
            ContractDomainMapping();
            DomainContractMapping();
        }

        protected abstract void ContractDomainMapping();

        protected abstract void DomainContractMapping();

        protected virtual void ConfigureSearchCache()
        {
            var configuredCacheName = ConfigurationManager.AppSettings["DistributedCacheName"] ?? "NexusMdmSearchCache";

            this.Container.RegisterType<ISearchCache, RegionedAppFabricSearchCache>(
                Name,
                new PerResolveLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<DataCache>(configuredCacheName),
                    new ResolvedParameter<ICacheItemPolicyFactory>(configuredCacheName),
                    Name));
        }

        protected void ConfigureValidation()
        {
            var entityType = typeof(TEntity);

            // Basic bits
            if (entityType == typeof(MDM.PartyRole) || (entityType.BaseType == typeof(MDM.PartyRole)))
            {
                this.Container.RegisterType<IValidator<CreateMappingRequest>, PartyRoleCreateMappingRequestValidator<TEntity, TMapping>>(
                Name,
                new InjectionConstructor(new ResolvedParameter<IValidatorEngine>(Name), 
                    new ResolvedParameter<IRepository>()));

                this.Container.RegisterType<IValidator<RWEST.Nexus.MDM.Contracts.NexusId>, PartyRoleNexusIdValidator<TMapping>>(Name);
                
                this.Container.RegisterType<IValidator<AmendMappingRequest>, PartyRoleAmendMappingRequestValidator<TEntity, TMapping>>(
               Name, new InjectionConstructor(new ResolvedParameter<IRepository>()));
            }
            else
            {
                this.Container.RegisterType<IValidator<CreateMappingRequest>, CreateMappingRequestValidator>(
                Name,
                new InjectionConstructor(new ResolvedParameter<IValidatorEngine>(Name)));

                this.Container.RegisterType<IValidator<RWEST.Nexus.MDM.Contracts.NexusId>, NexusIdValidator<TMapping>>(Name);
                
                this.Container.RegisterType<IValidator<AmendMappingRequest>, AmendMappingRequestValidator<TMapping>>(
               Name, new InjectionConstructor(new ResolvedParameter<IRepository>()));
            }

            this.Container.RegisterType<IValidator<MappingRequest>, MappingRequestValidator>(Name,
                new InjectionConstructor(
                    new ResolvedParameter<IValidatorEngine>(Name),
                    new ResolvedParameter<IRepository>()));

            // Factory
            // Do it this way as it's too nasty to inject string parameters at r/t with Unity
            var engine = new NamedLocatorValidatorEngine(Name, Container.Resolve<IServiceLocator>());
            this.Container.RegisterInstance(typeof(IValidatorEngine), Name, engine);

            this.Container.RegisterType<IValidator<TContract>, TContractValidator>(Name,
                new InjectionConstructor(
                    new ResolvedParameter<IValidatorEngine>(Name),
                    new ResolvedParameter<IRepository>()));
        }
    }
}