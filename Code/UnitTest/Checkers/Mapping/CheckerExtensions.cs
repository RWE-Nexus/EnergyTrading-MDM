namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading;
    using RWEST.Nexus.MDM;

    public static class CheckerExtensions
    {
        public static void Check(this Person expected, Person candidate)
        {
            Assert.AreEqual(expected.Id, candidate.Id, "Id differs");

            for (int i = 0; i < expected.Details.Count; i++)
            {
                expected.Details[i].Check(candidate.Details[i]);
            }
        }

        public static void Check(this PersonDetails expected, PersonDetails candidate)
        {
            Assert.AreEqual(expected.Id, candidate.Id, "Id differs");
            Assert.AreEqual(expected.FirstName, candidate.FirstName, "FirstName differs");
            Assert.AreEqual(expected.LastName, candidate.LastName, "LastName differs");
            Assert.AreEqual(expected.Email, candidate.Email, "Email differs");

            expected.Validity.Check(candidate.Validity);
        }

        public static void Check(this Party expected, Party candidate)
        {
            Assert.AreEqual(expected.Id, candidate.Id, "Id differs");

            for (int i = 0; i < expected.Details.Count; i++)
            {
                expected.Details[i].Check(candidate.Details[i]);
            }
        }

        public static void Check(this PartyDetails expected, PartyDetails candidate)
        {
            Assert.AreEqual(expected.Id, candidate.Id, "Id differs");
            Assert.AreEqual(expected.Name, candidate.Name, "Name differs");

            expected.Validity.Check(candidate.Validity);
        }
        public static void Check(this DateRange expected, DateRange candidate)
        {
            Assert.AreEqual(expected.Start, candidate.Start, "Start differs");
            Assert.AreEqual(expected.Finish, candidate.Finish, "Finish differs");
        }
    }
}
