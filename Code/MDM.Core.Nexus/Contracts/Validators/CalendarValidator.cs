using EnergyTrading.MDM.Contracts.Rules;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Validation;

    public class CalendarValidator : Validator<Calendar>
    {
        public CalendarValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new PredicateRule<Calendar>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new ChildCollectionRule<Calendar, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
        }
    }
}
		