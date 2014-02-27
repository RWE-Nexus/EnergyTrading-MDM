namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class BrokerCommodityValidator : Validator<BrokerCommodity>
    {
        public BrokerCommodityValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<BrokerCommodity, NexusId>(validatorEngine, p => p.Identifiers));
        }
    }
}