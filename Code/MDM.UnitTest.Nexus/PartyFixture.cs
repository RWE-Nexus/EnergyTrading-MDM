namespace EnergyTrading.MDM.Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading.MDM.Test.Data.EF;

    [TestClass]
    public class PartyFixture 
    {
        [TestMethod]
        public void VersionMinValueIfNoDetails()
        {
            var party = new Party();
            var entity = party as IEntity;

            Assert.AreEqual(long.MinValue, entity.Version, "Version differs");
        }

        [TestMethod]
        public void VersionReportsLatestTimestamp()
        {
            var party = new Party();
            var details = new PartyDetails() { Id = 12, Name = "Party 1", Timestamp = new byte[] { 34, 0, 0, 0, 0, 0, 0, 0 } };
            party.AddDetails(details);

            var entity = party as IEntity;

            Assert.AreEqual(34, entity.Version, "Version differs");
        }

        [TestMethod]
        public void AddingDetailsTrimsMappingToEntityFinish()
        {
            var start = new DateTime(2000, 1, 1);
            var finish = DateUtility.Round(SystemTime.UtcNow()).AddDays(3);

            var party = new Party();
            var m1 = new PartyMapping { Validity = new DateRange(start, DateTime.MaxValue) };
            party.ProcessMapping(m1);

            var d1 = new PartyDetails { Validity = new DateRange(start, finish) };
            party.AddDetails(d1);

            Assert.AreEqual(finish, m1.Validity.Finish, "Mapping finish differs");
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_the_valiidity_of_a_party
    {
        [TestMethod]
        public void should_return_the_min_and_max_date_of_all_party_details()
        {
            var range1 = new DateRange(DateTime.Today, DateTime.Today.AddDays(2));
            var range2 = new DateRange(DateTime.Today.AddDays(2), DateTime.Today.AddDays(4));

            var party = new Party();
            party.AddDetails(new PartyDetails() { Name = "Rob", Validity = range1 });
            party.AddDetails(new PartyDetails() { Name = "Bob", Validity = range2 });

            Assert.AreEqual(range1.Start, party.Validity.Start);
            Assert.AreEqual(range2.Finish, party.Validity.Finish);
        }
    }
}