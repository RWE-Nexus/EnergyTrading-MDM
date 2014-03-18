namespace EnergyTrading.MDM.Contracts.Validators
{
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class ProductTenorTypeValidator : Validator<ProductTenorType>
    {
        public ProductTenorTypeValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<ProductTenorType, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new NexusEntityExistsRule<ProductTenorType, MDM.Product, ProductMapping>(repository, x => x.Details.Product, true));
            Rules.Add(new NexusEntityExistsRule<ProductTenorType, MDM.TenorType, TenorTypeMapping>(repository, x => x.Details.TenorType, true));
        }
    }
}