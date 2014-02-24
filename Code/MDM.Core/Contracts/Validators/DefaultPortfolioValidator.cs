namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
    using EnergyTrading.Data;

    public class BookDefaultValidator : Validator<BookDefault>
    {
        public BookDefaultValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<BookDefault, NexusId>(validatorEngine, p => p.Identifiers));
        }
    }
}