namespace EnergyTrading.MDM.Test.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;

    public partial class ShipperCodeValidatorFixture
    {
		partial void AddRelatedEntities(RWEST.Nexus.MDM.Contracts.ShipperCode contract)
		{
		    contract.Details.Location = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "1" } };
		    contract.Details.Party = new EntityId() { Identifier = new NexusId()
		        {
		            IsNexusId = true, Identifier = "1"
		        } };
		}
    }
}
