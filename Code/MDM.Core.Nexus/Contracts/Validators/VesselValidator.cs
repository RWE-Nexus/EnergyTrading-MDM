namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class VesselValidator : Validator<Vessel>
    {
        public VesselValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Vessel, NexusId>(validatorEngine, p => p.Identifiers));
        }
    }
}