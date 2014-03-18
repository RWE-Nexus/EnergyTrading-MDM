using EnergyTrading.MDM.Contracts.Rules;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Validation;

    public class PartyRoleAccountabilityValidator : Validator<PartyRoleAccountability>
    {
        public PartyRoleAccountabilityValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<PartyRoleAccountability, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<PartyRoleAccountability>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<PartyRoleAccountability, MDM.PartyRole, PartyRoleMapping>(repository, x => x.Details.SourcePartyRole, false));
            Rules.Add(new NexusEntityExistsRule<PartyRoleAccountability, MDM.PartyRole, PartyRoleMapping>(repository, x => x.Details.TargetPartyRole, false));
        }
    }
}
