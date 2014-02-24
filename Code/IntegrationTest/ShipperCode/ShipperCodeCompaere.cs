namespace EnergyTrading.MDM.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RWEST.Nexus.MDM.Contracts;

    public static class ShipperCodeComparer
    {
        public static void Compare(RWEST.Nexus.MDM.Contracts.ShipperCode contract, MDM.ShipperCode entity)
        {
            Assert.AreEqual(contract.Details.Code, entity.Code);
            Assert.AreEqual(contract.Details.Location.NexusId(), entity.Location.Id);
            Assert.AreEqual(contract.Details.Party.NexusId(), entity.Party.Id);
        }
    }
}