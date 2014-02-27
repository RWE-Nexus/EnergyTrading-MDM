namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class BrokerRateValidator : Validator<BrokerRate>
    {
        public BrokerRateValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<BrokerRate, NexusId>(validatorEngine, p => p.Identifiers));
        }
    }
}