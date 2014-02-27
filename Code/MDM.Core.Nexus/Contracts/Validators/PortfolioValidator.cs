using EnergyTrading.MDM.Contracts.Rules;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class PortfolioValidator : Validator<Portfolio>
    {
        public PortfolioValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Portfolio, NexusId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<Portfolio>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new PredicateRule<Portfolio>(p => !string.IsNullOrWhiteSpace(p.Details.PortfolioType), "Portfolio Type must not be null or an empty string"));
        }
    }
}