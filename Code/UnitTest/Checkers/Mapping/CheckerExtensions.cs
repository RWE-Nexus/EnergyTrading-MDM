namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using EnergyTrading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class CheckerExtensions
    {
        public static void Check(this DateRange expected, DateRange candidate)
        {
            Assert.AreEqual(expected.Start, candidate.Start, "Start differs");
            Assert.AreEqual(expected.Finish, candidate.Finish, "Finish differs");
        }
    }
}
