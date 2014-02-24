namespace EnergyTrading.MDM.Messages.Validators
{
    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.Validation;

    public class AmendMappingRequestValidator<TMapping> : Validator<AmendMappingRequest>
        where TMapping : class, IEntityMapping
    {
        public AmendMappingRequestValidator(IRepository repository)
        {
            Rules.Add(new AmendMappingNoOverlappingRule<TMapping>(repository));
        }
    }
}