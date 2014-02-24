namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class TenorTypeValidator : Validator<TenorType>
    {
        public TenorTypeValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<TenorType, NexusId>(validatorEngine, p => p.Identifiers));
        }
    }
}