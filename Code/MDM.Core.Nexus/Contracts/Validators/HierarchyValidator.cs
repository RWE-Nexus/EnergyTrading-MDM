namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class HierarchyValidator : Validator<Hierarchy>
    {
        public HierarchyValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Hierarchy, NexusId>(validatorEngine, p => p.Identifiers));
        }
    }
}