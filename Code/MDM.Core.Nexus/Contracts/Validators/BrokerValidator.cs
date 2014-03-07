using EnergyTrading.MDM.Contracts.Rules;
using EnergyTrading.MDM.Data;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class BrokerValidator : Validator<Broker>
    {
        public BrokerValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Broker, NexusId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<Broker>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<RWEST.Nexus.MDM.Contracts.Broker, MDM.Party, MDM.PartyMapping>(repository, x => x.Party, true));
        }
    }
}