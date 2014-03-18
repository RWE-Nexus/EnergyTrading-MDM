namespace EnergyTrading.MDM.Contracts.Validators
{
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Validation;
    using EnergyTrading.Data;

    public class BookValidator : Validator<Book>
    {
        public BookValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Book, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
        }
    }
}