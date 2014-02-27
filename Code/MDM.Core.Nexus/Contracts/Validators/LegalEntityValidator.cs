namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
    using EnergyTrading.Data;

    public class LegalEntityValidator : Validator<LegalEntity>
    {
        public LegalEntityValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<LegalEntity, NexusId>(validatorEngine, p => p.Identifiers));
        }
    }
}