namespace EnergyTrading.MDM.Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class PartyDetailFixture
    {
        private TimeSpan interval = new TimeSpan(0, 0, 0, 1);

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNull()
        {
            var party = new Party();

            party.AddDetails(null);
        }

        [TestMethod]
        public void AddFirst()
        {
            var party = new Party();
            var details = new PartyDetails { Validity = DateRange.MaxDateRange };

            party.AddDetails(details);

            Assert.AreEqual(1, party.Details.Count, "Count differs");
            Assert.AreSame(details, party.LatestDetails, "Latest differs");
        }

        [TestMethod]
        public void AddTwoDetailsCompatibleRanges()
        {
            var party = new Party();
            var start = new DateTime(2000, 12, 31);
            var start2 = DateUtility.Round(SystemTime.UtcNow().AddDays(10));

            var d1 = new PartyDetails { Validity = new DateRange(start, start2) };
            var d2 = new PartyDetails { Validity = new DateRange(start2, start2.AddDays(3)) };

            party.AddDetails(d1);
            party.AddDetails(d2);

            Assert.AreEqual(2, party.Details.Count, "Count differs");
            Assert.AreEqual(start2.Add(-interval), d1.Validity.Finish, "Finish differs");
            Assert.AreSame(d2, party.LatestDetails, "Latest differs");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddTwoDetailsStartTooEarly()
        {
            var party = new Party();
            var start = new DateTime(2000, 12, 31);
            var start2 = DateUtility.Round(SystemTime.UtcNow().AddDays(20));

            var d1 = new PartyDetails { Validity = new DateRange(start, start2) };
            var d2 = new PartyDetails { Validity = new DateRange(start.AddDays(-3), start2.AddDays(3)) };

            // NB Bypass business rules
            party.Details.Add(d1);
            try
            {
                party.AddDetails(d2);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("Validity range starts on or before start of latest range"));
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]       
        public void AddTwoDetailsNotContiguous()
        {
            var party = new Party();
            var start = new DateTime(2000, 12, 31);
            var start2 = DateUtility.Round(SystemTime.UtcNow().AddDays(20));

            var d1 = new PartyDetails { Validity = new DateRange(start, start2) };
            var d2 = new PartyDetails { Validity = new DateRange(start2.AddDays(3), start2.AddDays(5)) };

            // NB Bypass business rules
            party.Details.Add(d1);
            try
            {
                party.AddDetails(d2);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("Validity range not contiguous with latest range"));
                throw;
            }
        }

        [TestMethod]
        public void AddTwoDetailsStartAndEndSame()
        {
            var party = new Party();
            var start = new DateTime(2000, 12, 31);
            var start2 = DateUtility.Round(SystemTime.UtcNow().AddDays(20));

            var d1 = new PartyDetails { Validity = new DateRange(start, start2), Name = "Detail One" };
            var d2 = new PartyDetails { Validity = new DateRange(start, start2), Name = "Detail Two" };

            // NB Bypass business rules
            party.Details.Add(d1);
            party.AddDetails(d2);

            Assert.AreEqual(1, party.Details.Count, "There should only be one detail");
            Assert.AreEqual("Detail Two", party.LatestDetails.Name, "The detail name should be updated");
        }

        [TestMethod]
        public void BringBackValidityOfMostRecentDetail()
        {
            var party = new Party();
            var start = new DateTime(2000, 12, 31);
            var start2 = DateUtility.Round(SystemTime.UtcNow().AddDays(20));

            var d1 = new PartyDetails { Validity = new DateRange(start, start2), Name = "Detail One" };
            var d2 = new PartyDetails { Validity = new DateRange(start, start2), Name = "Detail Two" };

            // NB Bypass business rules
            party.Details.Add(d1);
            party.AddDetails(d2);

            int oneDay = 1;
            d2.Validity = new DateRange(start, start2.Subtract(new TimeSpan(oneDay, 0, 0, 0)));
            party.AddDetails(d2);

            Assert.AreEqual(1, party.Details.Count, "There should only be one detail");
            Assert.AreEqual(start2.Subtract(new TimeSpan(oneDay, 0, 0, 0)), party.LatestDetails.Validity.Finish, "The finish date should have been updated");
        }
    }
}
