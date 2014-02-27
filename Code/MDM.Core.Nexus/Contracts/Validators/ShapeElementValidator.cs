namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class ShapeElementValidator : Validator<ShapeElement>
    {
        public ShapeElementValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<ShapeElement, NexusId>(validatorEngine, p => p.Identifiers));
        }
    }
}