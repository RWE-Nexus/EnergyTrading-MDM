namespace EnergyTrading.MDM.Contracts.Validators
{
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class BrokerRateValidator : Validator<BrokerRate>
    {
        public BrokerRateValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<BrokerRate, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
        }
    }
}