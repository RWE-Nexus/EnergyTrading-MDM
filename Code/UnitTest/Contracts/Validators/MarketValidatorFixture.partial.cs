namespace EnergyTrading.MDM.Test.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;

    public partial class MarketValidatorFixture
    {
		partial void AddRelatedEntities(RWEST.Nexus.MDM.Contracts.Market contract)
		{
		    contract.Details.Calendar = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "1" } };
		    contract.Details.Commodity = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "1" } };
		    contract.Details.Location = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "1" } };
		}
    }
}
