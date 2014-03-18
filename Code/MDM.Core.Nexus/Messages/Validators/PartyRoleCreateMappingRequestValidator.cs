namespace EnergyTrading.MDM.Messages.Validators
{
    using EnergyTrading.Data;
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.Validation;

    public class PartyRoleCreateMappingRequestValidator<TEntity, TMapping> : Validator<CreateMappingRequest> 
        where TMapping : class, IIdentifiable, IEntityMapping
        where TEntity : class, IIdentifiable, IEntity
    {
        public PartyRoleCreateMappingRequestValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            this.Rules.Add(new ChildRule<CreateMappingRequest, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Mapping));
            this.Rules.Add(new PartyRoleCreateMappingdNoOverlappingRule<TEntity, TMapping>(repository));
        }
    }
}