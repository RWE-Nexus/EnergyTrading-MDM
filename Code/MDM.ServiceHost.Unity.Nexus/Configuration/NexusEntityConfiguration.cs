namespace EnergyTrading.MDM.Configuration
{
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.MDM.Messages.Validators;
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