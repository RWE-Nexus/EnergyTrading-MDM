namespace EnergyTrading.MDM.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnergyTrading.Mdm.Contracts;

    public static class SourceSystemComparer
    {
        public static void Compare(EnergyTrading.Mdm.Contracts.SourceSystem contract, MDM.SourceSystem entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
            Assert.IsTrue(contract.Details.Parent == null && entity.Parent == null || contract.Details.Parent.MdmId() == entity.Parent.Id);
        }
    }
}