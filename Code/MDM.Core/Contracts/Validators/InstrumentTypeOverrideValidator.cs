namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class InstrumentTypeOverrideValidator : Validator<InstrumentTypeOverride>
    {
        public InstrumentTypeOverrideValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<InstrumentTypeOverride, NexusId>(validatorEngine, p => p.Identifiers));
        }
    }
}