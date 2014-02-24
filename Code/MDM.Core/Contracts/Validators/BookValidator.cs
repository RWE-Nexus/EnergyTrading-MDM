namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
    using EnergyTrading.Data;

    public class BookValidator : Validator<Book>
    {
        public BookValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Book, NexusId>(validatorEngine, p => p.Identifiers));
        }
    }
}