namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.Validation;

    public class ProductTypeInstanceValidator : Validator<ProductTypeInstance>
    {
        public ProductTypeInstanceValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<ProductTypeInstance, NexusId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<ProductTypeInstance>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<RWEST.Nexus.MDM.Contracts.ProductTypeInstance, MDM.ProductType, MDM.ProductTypeMapping>(repository, x => x.Details.ProductType, true));
        }
    }
}
		