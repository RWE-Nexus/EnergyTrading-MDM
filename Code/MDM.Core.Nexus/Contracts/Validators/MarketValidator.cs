namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.Validation;

    public class MarketValidator : Validator<Market>
    {
        public MarketValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Market, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<Market>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.Market, MDM.Location, MDM.LocationMapping>(repository, x => x.Details.Location, true));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.Market, MDM.Calendar, MDM.CalendarMapping>(repository, x => x.Details.Calendar, true));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.Market, MDM.Commodity, MDM.CommodityMapping>(repository, x => x.Details.Commodity, true));
        }
    }
}
		