namespace EnergyTrading.MDM.Test.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;

    public partial class CommodityInstrumentTypeValidatorFixture
    {
        partial void AddRelatedEntities(RWEST.Nexus.MDM.Contracts.CommodityInstrumentType contract)
        {
            contract.Details.Commodity = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "1" } };
        }
    }
}
