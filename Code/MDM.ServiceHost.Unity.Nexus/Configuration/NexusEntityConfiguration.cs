namespace MDM.ServiceHost.Unity.Nexus.Configuration
{
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.MDM.Messages.Validators;
    using EnergyTrading.MDM.ServiceHost.Unity.Configuration;
    using EnergyTrading.MDM.Services;
    using EnergyTrading.Validation;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    public abstract class NexusEntityConfiguration<TMdmService, TEntity, TContract, TMapping, TContractValidator> : EntityConfiguration<TMdmService, TEntity, TContract, TMapping, TContractValidator>
        where TMdmService : IMdmService<TContract, TEntity>
        where TContract : class
        where TEntity : class, IIdentifiable, IEntity
        where TMapping : class, IEntityMapping
        where TContractValidator : IValidator<TContract>
    {
        private IMappingEngine mappingEngine;

        protected NexusEntityConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override void ConfigureValidation()
        {
            var entityType = typeof(TEntity);

            // Basic bits
            if (entityType == typeof(EnergyTrading.MDM.PartyRole) || (entityType.BaseType == typeof(EnergyTrading.MDM.PartyRole)))
            {
                this.Container.RegisterType<IValidator<CreateMappingRequest>, PartyRoleCreateMappingRequestValidator<TEntity, TMapping>>(
                this.Name,
                new InjectionConstructor(new ResolvedParameter<IValidatorEngine>(this.Name), 
                    new ResolvedParameter<IRepository>()));

                this.Container.RegisterType<IValidator<EnergyTrading.Mdm.Contracts.MdmId>, PartyRoleNexusIdValidator<TMapping>>(this.Name);
                
                this.Container.RegisterType<IValidator<AmendMappingRequest>, PartyRoleAmendMappingRequestValidator<TEntity, TMapping>>(
               this.Name, new InjectionConstructor(new ResolvedParameter<IRepository>()));
            }
            else
            {
                this.Container.RegisterType<IValidator<CreateMappingRequest>, CreateMappingRequestValidator>(
                this.Name,
                new InjectionConstructor(new ResolvedParameter<IValidatorEngine>(this.Name)));

                this.Container.RegisterType<IValidator<EnergyTrading.Mdm.Contracts.MdmId>, NexusIdValidator<TMapping>>(this.Name);
                
                this.Container.RegisterType<IValidator<AmendMappingRequest>, AmendMappingRequestValidator<TMapping>>(
               this.Name, new InjectionConstructor(new ResolvedParameter<IRepository>()));
            }

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