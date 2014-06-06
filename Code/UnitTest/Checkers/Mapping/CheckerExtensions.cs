namespace EnergyTrading.Mdm.Test.Checkers.Mapping
{
    using EnergyTrading;

    using NUnit.Framework;

    public static class CheckerExtensions
    {
        public static void Check(this DateRange expected, DateRange candidate)
        {
            Assert.AreEqual(expected.Start, candidate.Start, "Start differs");
            Assert.AreEqual(expected.Finish, candidate.Finish, "Finish differs");
        }
    }
}
