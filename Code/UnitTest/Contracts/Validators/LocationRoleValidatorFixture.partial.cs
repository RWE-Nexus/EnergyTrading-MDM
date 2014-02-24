namespace EnergyTrading.MDM.Test.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;

    public partial class LocationRoleValidatorFixture
    {
		partial void AddRelatedEntities(RWEST.Nexus.MDM.Contracts.LocationRole contract)
		{
		    contract.Details.Location = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "1" } };
		}
    }
}