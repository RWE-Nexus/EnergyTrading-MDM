namespace EnergyTrading.MDM.Contracts.Validators
{
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class CommodityFeeTypeValidator : Validator<CommodityFeeType>
    {
        public CommodityFeeTypeValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<CommodityFeeType, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
        }
    }
}