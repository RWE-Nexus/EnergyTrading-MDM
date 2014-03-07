namespace EnergyTrading.MDM.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RWEST.Nexus.MDM.Contracts;

    public static class SourceSystemComparer
    {
        public static void Compare(RWEST.Nexus.MDM.Contracts.SourceSystem contract, MDM.SourceSystem entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
            Assert.IsTrue(contract.Details.Parent == null && entity.Parent == null || contract.Details.Parent.NexusId() == entity.Parent.Id);
        }
    }
}