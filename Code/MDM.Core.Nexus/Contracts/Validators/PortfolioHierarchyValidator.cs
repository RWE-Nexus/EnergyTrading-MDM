namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class PortfolioHierarchyValidator : Validator<PortfolioHierarchy>
    {
        public PortfolioHierarchyValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<PortfolioHierarchy, NexusId>(validatorEngine, p => p.Identifiers));
        }
    }
}