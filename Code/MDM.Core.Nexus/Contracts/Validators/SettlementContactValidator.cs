using EnergyTrading.MDM.Contracts.Rules;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class SettlementContactValidator : Validator<SettlementContact>
    {
        public SettlementContactValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<SettlementContact, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<SettlementContact>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.SettlementContact, MDM.CommodityInstrumentType, MDM.CommodityInstrumentTypeMapping>(repository, x => x.Details.CommodityInstrumentType, false));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.SettlementContact, MDM.Party, MDM.PartyMapping>(repository, x => x.Details.SourceParty, false));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.SettlementContact, MDM.Party, MDM.PartyMapping>(repository, x => x.Details.TargetParty, false));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.SettlementContact, MDM.Person, MDM.PersonMapping>(repository, x => x.Details.SourcePerson, false));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.SettlementContact, MDM.Person, MDM.PersonMapping>(repository, x => x.Details.TargetPerson, false));
        }
    }
}