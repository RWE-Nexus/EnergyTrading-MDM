using EnergyTrading.MDM.Contracts.Rules;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class BusinessUnitValidator : Validator<BusinessUnit>
    {
        public BusinessUnitValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<BusinessUnit, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<BusinessUnit>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<BusinessUnit, MDM.Location, LocationMapping>(repository, x => x.Details.TaxLocation, false));
            Rules.Add(new NexusEntityExistsRule<BusinessUnit, MDM.Party, PartyMapping>(repository, x => x.Party, true));
        }
    }
}