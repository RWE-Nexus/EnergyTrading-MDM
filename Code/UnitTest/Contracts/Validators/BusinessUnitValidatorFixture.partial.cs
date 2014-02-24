namespace EnergyTrading.MDM.Test.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;

    public partial class BusinessUnitValidatorFixture
    {
        partial void AddRelatedEntities(RWEST.Nexus.MDM.Contracts.BusinessUnit contract)
        {
            contract.Party = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "1" } };
        }
    }
}
