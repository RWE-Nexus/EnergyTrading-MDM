namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class ShapeDayValidator : Validator<ShapeDay>
    {
        public ShapeDayValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<ShapeDay, NexusId>(validatorEngine, p => p.Identifiers));
        }
    }
}