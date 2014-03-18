using EnergyTrading.MDM.Contracts.Rules;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class PartyAccountabilityValidator : Validator<PartyAccountability>
    {
        public PartyAccountabilityValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<PartyAccountability, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<PartyAccountability>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.PartyAccountability, MDM.Party, MDM.PartyMapping>(repository, x => x.Details.SourceParty, false));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.PartyAccountability, MDM.Party, MDM.PartyMapping>(repository, x => x.Details.TargetParty, false));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.PartyAccountability, MDM.Person, MDM.PersonMapping>(repository, x => x.Details.SourcePerson, false));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.PartyAccountability, MDM.Person, MDM.PersonMapping>(repository, x => x.Details.TargetPerson, false));
        }
    }
}
