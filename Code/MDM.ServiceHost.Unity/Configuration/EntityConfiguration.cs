namespace EnergyTrading.Mdm.ServiceHost.Unity.Configuration
{
    using System;
    using System.Collections.Generic;

    using EnergyTrading.Configuration;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts.Validators;
    using EnergyTrading.Mdm.Messages;
    using EnergyTrading.Mdm.Messages.Validators;
    using EnergyTrading.Mdm.Services;
    using EnergyTrading.Validation;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// Base configuration class for an entity service.
    /// </summary>
    /// <typeparam name="TMdmService"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TContract"></typeparam>
    /// <typeparam name="TMapping"></typeparam>
    /// <typeparam name="TContractValidator"></typeparam>
    public abstract class EntityConfiguration<TMdmService, TEntity, TContract, TMapping, TContractValidator> : IGlobalConfigurationTask
        where TMdmService : IMdmService<TContract, TEntity>
        where TContract : class
        where TEntity : class, IIdentifiable, IEntity
        where TMapping : class, IEntityMapping
        where TContractValidator : IValidator<TContract>
    {
        private IMappingEngine mappingEngine;

        /// <summary>
        /// Creates a new instance of the <see cref="EntityConfiguration{TMdmService, TEntity, TContract, TMapping, TContractValidator}"/> class.
        /// </summary>
        /// <param name="container"></param>
        protected EntityConfiguration(IUnityContainer container)
        {
            Container = container;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        protected abstract string Name { get; }

        public virtual IList<Type> DependsOn
        {
            get { return new List<Type> { typeof(SimpleMappingEngineConfiguration) }; }
        }

        /// <summary>
        /// Gets the container
        /// </summary>
        protected IUnityContainer Container { get; private set; }

        /// <summary>
        /// Gets or sets the mapping engine.
        /// </summary>
        protected IMappingEngine MappingEngine
        {
            get { return mappingEngine ?? (mappingEngine = Container.Resolve<IMappingEngine>()); }
            set { mappingEngine = value; }
        }

        /// <summary>
        /// Configure the services, validation and mapping classes.
        /// </summary>
        public void Configure()
        {
            var registrar = new CacheRegistrar(Container);
            registrar.RegisterCachedMdmService<TContract, TEntity, TMdmService>(Name);

            ConfigureValidation();
            ConfigureMapping();
            OnConfigure();
        }

        /// <summary>
        /// Custom registration logic
        /// </summary>
        protected virtual void OnConfigure()
        {
        }

        /// <summary>
        /// Configure the mapping registration
        /// </summary>
        protected void ConfigureMapping()
        {
            ContractDomainMapping();
            DomainContractMapping();
        }

        protected abstract void ContractDomainMapping();

        protected abstract void DomainContractMapping();

        protected virtual void ConfigureValidation()
        {
            Container.RegisterType<IValidator<CreateMappingRequest>, CreateMappingRequestValidator>(
                Name,
                new InjectionConstructor(new ResolvedParameter<IValidatorEngine>(Name)));

            Container.RegisterType<IValidator<EnergyTrading.Mdm.Contracts.MdmId>, NexusIdValidator<TMapping>>(Name);
                
            Container.RegisterType<IValidator<AmendMappingRequest>, AmendMappingRequestValidator<TMapping>>(
                Name, new InjectionConstructor(new ResolvedParameter<IRepository>()));

            Container.RegisterType<IValidator<MappingRequest>, MappingRequestValidator>(Name,
                new InjectionConstructor(
                    new ResolvedParameter<IValidatorEngine>(Name),
                    new ResolvedParameter<IRepository>()));

            // Factory
            // Do it way as it's too nasty to inject string parameters at r/t with Unity
            var engine = new NamedLocatorValidatorEngine(Name, Container.Resolve<IServiceLocator>());
            Container.RegisterInstance(typeof(IValidatorEngine), Name, engine);

            Container.RegisterType<IValidator<TContract>, TContractValidator>(Name,
                new InjectionConstructor(
                    new ResolvedParameter<IValidatorEngine>(Name),
                    new ResolvedParameter<IRepository>()));
        }
    }
}