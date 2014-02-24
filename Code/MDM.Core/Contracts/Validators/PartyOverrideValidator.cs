namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class PartyOverrideValidator : Validator<PartyOverride>
    {
        public PartyOverrideValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<PartyOverride, NexusId>(validatorEngine, p => p.Identifiers));
        }
    }
}