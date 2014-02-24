namespace EnergyTrading.MDM.Test.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;

    public partial class ProductValidatorFixture
    {
        partial void AddRelatedEntities(RWEST.Nexus.MDM.Contracts.Product contract)
        {
            contract.Details.Name = "Test";
            contract.Details.Market = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "1" } };
        }
    }
}
