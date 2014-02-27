namespace EnergyTrading.MDM.Test.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;

    public partial class ProductTypeInstanceValidatorFixture
    {
		partial void AddRelatedEntities(RWEST.Nexus.MDM.Contracts.ProductTypeInstance contract)
		{
		    contract.Details.ProductType = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "1" } };
		}
    }
}

