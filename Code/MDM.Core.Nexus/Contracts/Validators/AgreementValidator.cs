namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;

    public class AgreementValidator : Validator<Agreement>
    {
        public AgreementValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Agreement, NexusId>(validatorEngine, p => p.Identifiers));
        }
    }
}