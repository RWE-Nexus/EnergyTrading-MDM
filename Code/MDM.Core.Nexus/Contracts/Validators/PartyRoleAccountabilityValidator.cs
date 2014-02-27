using EnergyTrading.MDM.Contracts.Rules;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;

    public class PartyRoleAccountabilityValidator : Validator<PartyRoleAccountability>
    {
        public PartyRoleAccountabilityValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<PartyRoleAccountability, NexusId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<PartyRoleAccountability>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<PartyRoleAccountability, MDM.PartyRole, PartyRoleMapping>(repository, x => x.Details.SourcePartyRole, false));
            Rules.Add(new NexusEntityExistsRule<PartyRoleAccountability, MDM.PartyRole, PartyRoleMapping>(repository, x => x.Details.TargetPartyRole, false));
        }
    }
}
