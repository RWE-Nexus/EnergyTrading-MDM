using EnergyTrading.MDM.Contracts.Rules;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class CurveValidator : Validator<Curve>
    {
        public CurveValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Curve, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<Curve>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new PredicateRule<Curve>(p => !string.IsNullOrWhiteSpace(p.Details.CurveType), "Curve Type must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.Curve, MDM.Commodity, MDM.CommodityMapping>(repository, x => x.Details.Commodity, true));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.Curve, MDM.Location, MDM.LocationMapping>(repository, x => x.Details.Location, false));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.Curve, MDM.Party, MDM.PartyMapping>(repository, x => x.Details.Originator, false));
        }
    }
}