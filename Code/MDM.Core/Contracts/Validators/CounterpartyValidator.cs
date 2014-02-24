using EnergyTrading.MDM.Contracts.Rules;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class CounterpartyValidator : Validator<Counterparty>
    {
        public CounterpartyValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Counterparty, NexusId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<Counterparty>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<RWEST.Nexus.MDM.Contracts.Counterparty, MDM.Party, MDM.PartyMapping>(repository, x => x.Party, true));
        }
    }
}