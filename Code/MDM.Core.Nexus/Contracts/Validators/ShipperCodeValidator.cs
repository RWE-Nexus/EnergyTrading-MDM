namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.Validation;

    public class ShipperCodeValidator : Validator<ShipperCode>
    {
        public ShipperCodeValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<ShipperCode, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.ShipperCode, MDM.Party, MDM.PartyMapping>(repository, x => x.Details.Party, true));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.ShipperCode, MDM.Location, MDM.LocationMapping>(repository, x => x.Details.Location, true));
            Rules.Add(new PredicateRule<ShipperCode>(p => !string.IsNullOrWhiteSpace(p.Details.Code), "Code must not be null or an empty string"));
        }
    }
}
		