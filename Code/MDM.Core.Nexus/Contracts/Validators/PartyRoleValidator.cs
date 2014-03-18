using EnergyTrading.MDM.Contracts.Rules;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Validation;

    public class PartyRoleValidator : Validator<PartyRole>
    {
        public PartyRoleValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<PartyRole, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<PartyRole>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new PredicateRule<PartyRole>(p => !string.IsNullOrWhiteSpace(p.PartyRoleType), "PartyRoleType must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.PartyRole, MDM.Party, MDM.PartyMapping>(repository, x => x.Party, true));
        }
    }
}
