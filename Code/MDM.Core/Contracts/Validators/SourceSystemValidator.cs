using EnergyTrading.MDM.Data;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.Validation;

    public class SourceSystemValidator : Validator<SourceSystem>
    {
        public SourceSystemValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<SourceSystem, NexusId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<SourceSystem>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<RWEST.Nexus.MDM.Contracts.SourceSystem, MDM.SourceSystem, MDM.SourceSystemMapping>(repository, x => x.Details.Parent, false));
            Rules.Add(new ParentDiffersRule<RWEST.Nexus.MDM.Contracts.SourceSystem, MDM.SourceSystem, MDM.SourceSystemMapping>(repository, x => x.Details.Name, x => x.Details.Parent, y => y.Name));
        }
    }
}
		