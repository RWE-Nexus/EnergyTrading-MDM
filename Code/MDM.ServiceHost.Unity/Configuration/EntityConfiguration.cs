namespace EnergyTrading.MDM.ServiceHost.Unity.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    using EnergyTrading.Caching;
    using EnergyTrading.Caching.AppFabric.Search;
    using EnergyTrading.Configuration;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.MDM.Messages.Validators;
    using EnergyTrading.MDM.Services;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    using Microsoft.ApplicationServer.Caching;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

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
                if (this.mappingEngine == null)
                {
                    this.mappingEngine = this.Container.Resolve<IMappingEngine>();
                }
                return this.mappingEngine;
            }
            set { this.mappingEngine = value; }
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
            CacheRegistrar.Instance.RegisterCachedMdmService<TContract, TEntity, TMdmService>(this.Name);
        }

        protected void ConfigureMapping()
        {
            this.ContractDomainMapping();
            this.DomainContractMapping();
        }

        protected abstract void ContractDomainMapping();

        protected abstract void DomainContractMapping();

        protected virtual void ConfigureSearchCache()
        {
            var configuredCacheName = ConfigurationManager.AppSettings["DistributedCacheName"] ?? "NexusMdmSearchCache";

            this.Container.RegisterType<ISearchCache, RegionedAppFabricSearchCache>(
                this.Name,
                new PerResolveLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<DataCache>(configuredCacheName),
                    new ResolvedParameter<ICacheItemPolicyFactory>(configuredCacheName),
                    this.Name));
        }

        protected virtual void ConfigureValidation()
        {
            this.Container.RegisterType<IValidator<CreateMappingRequest>, CreateMappingRequestValidator>(
            this.Name,
            new InjectionConstructor(new ResolvedParameter<IValidatorEngine>(this.Name)));

            this.Container.RegisterType<IValidator<RWEST.Nexus.MDM.Contracts.NexusId>, NexusIdValidator<TMapping>>(this.Name);
                
            this.Container.RegisterType<IValidator<AmendMappingRequest>, AmendMappingRequestValidator<TMapping>>(
            this.Name, new InjectionConstructor(new ResolvedParameter<IRepository>()));

            this.Container.RegisterType<IValidator<MappingRequest>, MappingRequestValidator>(this.Name,
                new InjectionConstructor(
                    new ResolvedParameter<IValidatorEngine>(this.Name),
                    new ResolvedParameter<IRepository>()));

            // Factory
            // Do it this way as it's too nasty to inject string parameters at r/t with Unity
            var engine = new NamedLocatorValidatorEngine(this.Name, this.Container.Resolve<IServiceLocator>());
            this.Container.RegisterInstance(typeof(IValidatorEngine), this.Name, engine);

            this.Container.RegisterType<IValidator<TContract>, TContractValidator>(this.Name,
                new InjectionConstructor(
                    new ResolvedParameter<IValidatorEngine>(this.Name),
                    new ResolvedParameter<IRepository>()));
        }
    }
}