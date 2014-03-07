namespace EnergyTrading.MDM.Test.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;

    public partial class ProductTypeValidatorFixture
    {
		partial void AddRelatedEntities(RWEST.Nexus.MDM.Contracts.ProductType contract)
		{
		    contract.Details.Product= new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "1" } };
		}
    }
}