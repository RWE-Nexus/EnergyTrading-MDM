namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class ShapeValidator : Validator<Shape>
    {
        public ShapeValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Shape, NexusId>(validatorEngine, p => p.Identifiers));
        }
    }
}