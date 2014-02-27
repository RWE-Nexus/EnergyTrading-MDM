namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class CommodityFeeTypeValidator : Validator<CommodityFeeType>
    {
        public CommodityFeeTypeValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<CommodityFeeType, NexusId>(validatorEngine, p => p.Identifiers));
        }
    }
}