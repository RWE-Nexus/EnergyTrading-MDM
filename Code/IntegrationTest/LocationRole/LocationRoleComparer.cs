namespace EnergyTrading.MDM.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RWEST.Nexus.MDM.Contracts;

    public static class LocationRoleComparer
    {
        public static void Compare(RWEST.Nexus.MDM.Contracts.LocationRole contract, MDM.LocationRole entity)
        {
            Assert.AreEqual(contract.Details.Location.NexusId(), entity.Location.Id);
            Assert.AreEqual(contract.Details.Type, entity.Type.Name);
        }
    }
}
