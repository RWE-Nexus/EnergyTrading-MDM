namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class ProductScotaValidator : Validator<ProductScota>
    {
        public ProductScotaValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<ProductScota, NexusId>(validatorEngine, p => p.Identifiers));
        }
    }
}