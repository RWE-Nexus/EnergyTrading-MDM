namespace EnergyTrading.MDM.Messages.Validators
{
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.Validation;

    public class PartyRoleAmendMappingRequestValidator<TEntity, TMapping> : Validator<AmendMappingRequest>
        where TMapping : class, IIdentifiable, IEntityMapping
        where TEntity : class, IIdentifiable, IEntity
    {
        public PartyRoleAmendMappingRequestValidator(IRepository repository)
        {
            this.Rules.Add(new PartyRoleAmendMappingNoOverlappingRule<TEntity, TMapping>(repository));
        }
    }
}