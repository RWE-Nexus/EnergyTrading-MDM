namespace EnergyTrading.Mdm.Contracts.Validators
{
    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Mdm.Contracts.Rules;
    using EnergyTrading.Validation;

    public class SourceSystemValidator : Validator<SourceSystem>
    {
        public SourceSystemValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<SourceSystem, MdmId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<SourceSystem>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<EnergyTrading.Mdm.Contracts.SourceSystem, Mdm.SourceSystem, Mdm.SourceSystemMapping>(repository, x => x.Details.Parent, false));
            Rules.Add(new ParentDiffersRule<EnergyTrading.Mdm.Contracts.SourceSystem, Mdm.SourceSystem, Mdm.SourceSystemMapping>(repository, x => x.Details.Name, x => x.Details.Parent, y => y.Name));
        }
    }
}