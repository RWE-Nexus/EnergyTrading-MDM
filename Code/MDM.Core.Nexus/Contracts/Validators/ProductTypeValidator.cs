namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.Validation;

    public class ProductTypeValidator : Validator<ProductType>
    {
        public ProductTypeValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<ProductType, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<ProductType>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.ProductType, MDM.Product, MDM.ProductMapping>(repository, x => x.Details.Product, true));
            Rules.Add(new PredicateRule<ProductType>(type => !string.IsNullOrWhiteSpace(type.Details.DeliveryRangeType), "Delivery Range Type must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.ProductType, MDM.Curve, MDM.CurveMapping>(repository, x => x.Details.SettlementCurve, false));
        }
    }
}
		