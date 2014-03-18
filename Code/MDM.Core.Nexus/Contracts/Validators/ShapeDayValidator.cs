namespace EnergyTrading.MDM.Contracts.Validators
{
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class ShapeDayValidator : Validator<ShapeDay>
    {
        public ShapeDayValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<ShapeDay, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
        }
    }
}