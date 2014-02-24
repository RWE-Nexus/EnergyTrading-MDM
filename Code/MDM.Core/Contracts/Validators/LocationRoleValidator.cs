namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.Validation;

    public class LocationRoleValidator : Validator<LocationRole>
    {
        public LocationRoleValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<LocationRole, NexusId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new NexusEntityExistsRule<RWEST.Nexus.MDM.Contracts.LocationRole, MDM.Location, MDM.LocationMapping>(repository, x => x.Details.Location, true));        }
    }
}
		