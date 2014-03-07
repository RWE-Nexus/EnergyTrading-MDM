namespace EnergyTrading.MDM.Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading;
    using RWEST.Nexus.MDM;

    [TestClass]
    public class PersonFixture 
    {
        [TestMethod]
        public void VersionMinValueIfNoDetails()
        {
            var person = new Person();
            var entity = person as IEntity;

            Assert.AreEqual(long.MinValue, entity.Version, "Version differs");
        }

        [TestMethod]
        public void VersionReportsLatestTimestamp()
        {
            var person = new Person();
            var details = new PersonDetails { Id = 12, FirstName = "John", LastName = "Smith", Timestamp = new byte[] { 34, 0, 0, 0, 0, 0, 0, 0 } };
            person.AddDetails(details);

            var entity = person as IEntity;

            Assert.AreEqual(34, entity.Version, "Version differs");
        }

        [TestMethod]
        public void AddingDetailsTrimsMappingToEntityFinish()
        {
            var start = new DateTime(2000, 1, 1);
            var finish = DateUtility.Round(SystemTime.UtcNow()).AddDays(3);

            var person = new Person();
            var m1 = new PersonMapping { Validity = new DateRange(start, DateTime.MaxValue) };
            person.ProcessMapping(m1);

            var d1 = new PersonDetails { Validity = new DateRange(start, finish) };
            person.AddDetails(d1);

            Assert.AreEqual(finish, m1.Validity.Finish, "Mapping finish differs");
        }

    [TestClass]
    public class when_a_request_is_made_for_the_valiidity_of_a_person
    {
        [TestMethod]
        public void should_return_the_min_and_max_date_of_all_person_details()
        {
            var range1 = new DateRange(DateTime.Today, DateTime.Today.AddDays(2));
            var range2 = new DateRange(DateTime.Today.AddDays(2), DateTime.Today.AddDays(4));

            var person = new Person();
            person.AddDetails(new PersonDetails() { FirstName = "Rob", Email = "test@test.com", Validity = range1 });
            person.AddDetails(new PersonDetails() { FirstName = "Bob", Email = "test@test.com", Validity = range2 });

            Assert.AreEqual(range1.Start, person.Validity.Start);
            Assert.AreEqual(range2.Finish, person.Validity.Finish);
        }
    }
    }
}