using EnergyTrading.MDM.Contracts.Rules;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class UnitValidator : Validator<Unit>
    {
        public UnitValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Unit, NexusId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<Unit>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<RWEST.Nexus.MDM.Contracts.Unit, MDM.Dimension, MDM.DimensionMapping>(repository, x => x.Details.Dimension, false));
        }
    }
}