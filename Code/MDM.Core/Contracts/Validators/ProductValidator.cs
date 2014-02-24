using EnergyTrading.MDM.Contracts.Rules;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;

    public class ProductValidator : Validator<Product>
    {
        public ProductValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Product, NexusId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<Product>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<RWEST.Nexus.MDM.Contracts.Product, MDM.Market, MDM.MarketMapping>(repository, x => x.Details.Market, true));
            Rules.Add(new NexusEntityExistsRule<RWEST.Nexus.MDM.Contracts.Product, MDM.CommodityInstrumentType, MDM.CommodityInstrumentTypeMapping>(repository, x => x.Details.CommodityInstrumentType, false));
            Rules.Add(new NexusEntityExistsRule<RWEST.Nexus.MDM.Contracts.Product, MDM.Curve, MDM.CurveMapping>(repository, x => x.Details.DefaultCurve, false));
        }
    }
}
		