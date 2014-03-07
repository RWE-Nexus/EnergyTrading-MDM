namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class ProductCurveValidator : Validator<ProductCurve>
    {
        public ProductCurveValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<ProductCurve, NexusId>(validatorEngine, p => p.Identifiers));
        }
    }
}