using System.Linq;
using EnergyTrading.MDM.Contracts.Rules;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;

    public class PartyValidator : Validator<Party>
    {
        public PartyValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Party, NexusId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<Party>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));

            //Rules.Add(new EntityNoOverlappingRule<Party>(repository, p=>p.ToNexusKey(), p => p.Details.Name, p => p.Nexus.StartDate, p => p.Nexus.EndDate));
        }
    }
}