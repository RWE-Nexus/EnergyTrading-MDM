namespace EnergyTrading.MDM.Contracts.Validators
{
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class ShapeValidator : Validator<Shape>
    {
        public ShapeValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Shape, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
        }
    }
}