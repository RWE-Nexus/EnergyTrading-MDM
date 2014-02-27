namespace EnergyTrading.MDM.Test.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;

    public partial class BrokerValidatorFixture
    {
        partial void AddRelatedEntities(RWEST.Nexus.MDM.Contracts.Broker contract)
        {
            contract.Party = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "1" } };
        }
    }
}
