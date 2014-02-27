namespace EnergyTrading.MDM.Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class PersonDetailFixture
    {
        private TimeSpan interval = new TimeSpan(0, 0, 0, 1);

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNull()
        {
            var person = new Person();

            person.AddDetails(null);
        }

        [TestMethod]
        public void AddFirst()
        {
            var person = new Person();
            var details = new PersonDetails { Validity = DateRange.MaxDateRange };

            person.AddDetails(details);

            Assert.AreEqual(1, person.Details.Count, "Count differs");
            Assert.AreSame(details, person.LatestDetails, "Latest differs");
        }

        [TestMethod]
        public void AddTwoDetailsCompatibleRanges()
        {
            var person = new Person();
            var start = new DateTime(2000, 12, 31);
            var start2 = DateUtility.Round(SystemTime.UtcNow().AddDays(10));

            var d1 = new PersonDetails { Validity = new DateRange(start, start2) };
            var d2 = new PersonDetails { Validity = new DateRange(start2, start2.AddDays(3)) };

            person.AddDetails(d1);
            person.AddDetails(d2);

            Assert.AreEqual(2, person.Details.Count, "Count differs");
            Assert.AreEqual(start2.Add(-interval), d1.Validity.Finish, "Finish differs");
            Assert.AreSame(d2, person.LatestDetails, "Latest differs");
        }

        [TestMethod]
        public void AddTwoDetailsOVerlapRanges()
        {
            var person = new Person();
            var start = new DateTime(2000, 12, 31);
            var start2 = DateUtility.Round(SystemTime.UtcNow().AddDays(10));
            var finish = start2.AddDays(5);

            var d1 = new PersonDetails { Validity = new DateRange(start, finish) };
            var d2 = new PersonDetails { Validity = new DateRange(start2, start2.AddDays(10)) };

            person.AddDetails(d1);
            person.AddDetails(d2);

            Assert.AreEqual(2, person.Details.Count, "Count differs");
            Assert.AreEqual(start2.Add(-interval), d1.Validity.Finish, "Finish differs");
            Assert.AreSame(d2, person.LatestDetails, "Latest differs");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddTwoDetailsStartTooEarly()
        {
            var person = new Person();
            var start = new DateTime(2000, 12, 31);
            var start2 = DateUtility.Round(SystemTime.UtcNow().AddDays(20));

            var d1 = new PersonDetails { Validity = new DateRange(start, start2) };
            var d2 = new PersonDetails { Validity = new DateRange(start.AddDays(-3), start2.AddDays(3)) };

            // NB Bypass business rules
            person.Details.Add(d1);
            try
            {
                person.AddDetails(d2);
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
            var person = new Person();
            var start = new DateTime(2000, 12, 31);
            var start2 = DateUtility.Round(SystemTime.UtcNow().AddDays(20));

            var d1 = new PersonDetails { Validity = new DateRange(start, start2) };
            var d2 = new PersonDetails { Validity = new DateRange(start2.AddDays(3), start2.AddDays(5)) };

            // NB Bypass business rules
            person.Details.Add(d1);
            try
            {
                person.AddDetails(d2);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("Validity range not contiguous with latest range"));
                throw;
            }
        }

        [TestMethod]
        public void BringBackValidityOfMostRecentDetail()
        {
            var person = new Person();
            var start = new DateTime(2000, 12, 31);
            var start2 = DateUtility.Round(SystemTime.UtcNow().AddDays(20));

            var d1 = new PersonDetails { Validity = new DateRange(start, start2), FirstName = "Detail One", Email = "test@test.com" };
            var d2 = new PersonDetails { Validity = new DateRange(start, start2), FirstName = "Detail Two", Email = "test@test.com" };

            // NB Bypass business rules
            person.Details.Add(d1);
            person.AddDetails(d2);

            int oneDay = 1;
            d2.Validity = new DateRange(start, start2.Subtract(new TimeSpan(oneDay, 0, 0, 0)));
            person.AddDetails(d2);

            Assert.AreEqual(1, person.Details.Count, "There should only be one detail");
            Assert.AreEqual(start2.Subtract(new TimeSpan(oneDay, 0, 0, 0)), person.LatestDetails.Validity.Finish, "The finish date should have been updated");
        }
    }
}