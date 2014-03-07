namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class FeeTypeValidator : Validator<FeeType>
    {
        public FeeTypeValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<FeeType, NexusId>(validatorEngine, p => p.Identifiers));
        }
    }
}