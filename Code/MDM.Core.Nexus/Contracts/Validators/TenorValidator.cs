namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class TenorValidator : Validator<Tenor>
    {
        public TenorValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Tenor, NexusId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<Tenor>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<Tenor, MDM.TenorType, TenorTypeMapping>(repository, x => x.Details.TenorType, true));
        }
    }
}