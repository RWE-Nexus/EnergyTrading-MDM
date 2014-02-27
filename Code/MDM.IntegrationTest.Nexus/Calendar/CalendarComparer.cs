namespace EnergyTrading.MDM.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class CalendarComparer
    {
        public static void Compare(RWEST.Nexus.MDM.Contracts.Calendar contract, Calendar entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
            Assert.AreEqual(contract.Details.CalendayDays.Count, entity.Days.Count);
        }
    }
}