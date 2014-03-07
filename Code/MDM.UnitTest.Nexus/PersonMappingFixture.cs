namespace EnergyTrading.MDM.Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class PersonMappingFixture : EntityMappingFixture
    {
        [TestMethod]
        public void ConvertToNexusId()
        {
            this.ConvertToNexusId<PersonMapping>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNull()
        {
            var entity = new Person();            

            entity.ProcessMapping(null);
        }

        [TestMethod]
        public void AddFirst()
        {
            var entity = new Person();
            var system = new SourceSystem { Name = "Test" };
            var mapping = new PersonMapping { System = system, MappingValue = "1", Validity = DateRange.MaxDateRange };

            entity.ProcessMapping(mapping);
            
            Assert.AreEqual(1, entity.Mappings.Count, "Count differs");
        }

        [TestMethod]
        public void AddTwoDifferentSystems()
        {
            var entity = new Person();
            var s1 = new SourceSystem { Name = "Test" };
            var m1 = new PersonMapping { System = s1, MappingValue = "1", Validity = DateRange.MaxDateRange };
            var s2 = new SourceSystem { Name = "Bob" };
            var m2 = new PersonMapping { System = s2, MappingValue = "1", Validity = DateRange.MaxDateRange };

            entity.ProcessMapping(m1);
            entity.ProcessMapping(m2);

            Assert.AreEqual(2, entity.Mappings.Count, "Count differs");           
        }

        [TestMethod]
        public void AddTwoDifferentValidity()
        {
            var start = new DateTime(2000, 12, 31);
            var start2 = new DateTime(2010, 1, 1);
            var entity = new Person();

            var s1 = new SourceSystem { Name = "Test" };
            var m1 = new PersonMapping { System = s1, MappingValue = "1", Validity = new DateRange(start, start.AddDays(3)) };
            var m2 = new PersonMapping { System = s1, MappingValue = "1", Validity = new DateRange(start2, DateUtility.MaxDate) };

            entity.ProcessMapping(m1);
            entity.ProcessMapping(m2);

            Assert.AreEqual(2, entity.Mappings.Count, "Count differs");
            Assert.AreEqual(start2.Add(-Interval), m1.Validity.Finish, "Finish differs");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddTwoOverlapValidity()
        {
            var start = new DateTime(2000, 12, 31);
            var start2 = new DateTime(2010, 1, 1);
            var entity = new Person();

            var s1 = new SourceSystem { Name = "Test" };
            var m1 = new PersonMapping { System = s1, MappingValue = "1", Validity = new DateRange(start, DateUtility.MaxDate) };
            var m2 = new PersonMapping { System = s1, MappingValue = "1", Validity = new DateRange(start2, DateUtility.MaxDate) };
            var m3 = new PersonMapping { System = s1, MappingValue = "1", Validity = new DateRange(start.AddDays(2), DateUtility.MaxDate) };

            entity.ProcessMapping(m1);
            entity.ProcessMapping(m2);
            try
            {
                entity.ProcessMapping(m3);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("Validity range starts on or before start of latest range"));
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateNonExistentMapping()
        {
            var entity = new Person();
            var details = new PersonDetails { Id = 12, FirstName = "John", LastName = "Smith" };
            entity.AddDetails(details);

            var mapping = new PersonMapping { Id = 34 };

            entity.ProcessMapping(mapping);
        }

        [TestMethod]
        public void UpdateMappingAdjustsEndDate()
        {
            var start = new DateTime(2000, 12, 31);
            var finish = DateUtility.Round(SystemTime.UtcNow().AddDays(5));
            var entity = new Person();

            var s1 = new SourceSystem { Name = "Test" };
            var m1 = new PersonMapping { Id = 12, System = s1, MappingValue = "1", Validity = new DateRange(start, DateUtility.MaxDate) };
            var m2 = new PersonMapping { Id = 12, System = s1, MappingValue = "1", Validity = new DateRange(start, finish) };

            // NB We deliberately bypasses the business logic
            m1.Person = entity;
            entity.Mappings.Add(m1);

            entity.ProcessMapping(m2);

            Assert.AreEqual(finish, m1.Validity.Finish, "Finish differs");
        }

        [TestMethod]
        public void ChangeEndDateConstraintedToEntityValidity()
        {
            var start = new DateTime(2000, 12, 31);
            var finish = DateUtility.Round(SystemTime.UtcNow().AddDays(5));
            var entity = new Person();

            var d1 = new PersonDetails { Validity = new DateRange(start, finish) };
            var s1 = new SourceSystem { Name = "Test" }; 
            var m1 = new PersonMapping { System = s1, MappingValue = "1", Validity = new DateRange(start, DateUtility.MaxDate) };

            entity.AddDetails(d1);
            entity.ProcessMapping(m1);

            m1.ChangeEndDate(finish.AddDays(5));
 
            Assert.AreEqual(finish, m1.Validity.Finish, "Finish differs");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateIncompatibleMapping()
        {
            var start = new DateTime(2000, 12, 31);
            var finish = DateUtility.Round(SystemTime.UtcNow().AddDays(5));
            var entity = new Person();

            var s1 = new SourceSystem { Name = "Test" };
            var m1 = new PersonMapping { Id = 12, System = s1, MappingValue = "1", Validity = new DateRange(start, DateUtility.MaxDate) };
            var m2 = new PersonMapping { Id = 12, System = s1, MappingValue = "2", Validity = new DateRange(start, finish) };

            // NB We deliberately bypasses the business logic
            m1.Person = entity;
            entity.Mappings.Add(m1);
            try
            {
                entity.ProcessMapping(m2);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("Mapping not compatible"));
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void AddMappingToExpiredEntityNotAllowed()
        {
            var start = new DateTime(2000, 12, 31);
            var finish = DateUtility.Round(SystemTime.UtcNow().AddDays(-1));
            var entity = new Person();

            var d1 = new PersonDetails
            {
                Person = entity,
                Validity = new DateRange(start, finish)
            };
            // NB Must bypass business rules to set up
            entity.Details.Add(d1);

            var s1 = new SourceSystem { Name = "Test" };
            var m1 = new PersonMapping { System = s1, MappingValue = "1", Validity = new DateRange(start, DateUtility.MaxDate) };

            try
            {
                entity.ProcessMapping(m1);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("Cannot change mapping for expired entity"));
                throw;
            }
        }
    }
}