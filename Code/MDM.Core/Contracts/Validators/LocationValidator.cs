namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.Validation;
    using EnergyTrading.MDM.Data;

    public class LocationValidator : Validator<Location>
    {
        public LocationValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Location, NexusId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<Location>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<RWEST.Nexus.MDM.Contracts.Location, MDM.Location, MDM.LocationMapping>(repository, x => x.Details.Parent, false));
            Rules.Add(new ParentDiffersRule<RWEST.Nexus.MDM.Contracts.Location, MDM.Location, MDM.LocationMapping>(repository, x => x.Details.Name, x => x.Details.Parent, y => y.Name));
        }
    }
}
		