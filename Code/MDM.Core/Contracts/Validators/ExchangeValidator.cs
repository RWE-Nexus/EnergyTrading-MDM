using EnergyTrading.MDM.Contracts.Rules;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class ExchangeValidator : Validator<Exchange>
    {
        public ExchangeValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Exchange, NexusId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<Exchange>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<RWEST.Nexus.MDM.Contracts.Exchange, MDM.Party, MDM.PartyMapping>(repository, x => x.Party, true));
        }
    }
}