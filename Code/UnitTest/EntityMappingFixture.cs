namespace EnergyTrading.Mdm.Test
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm;
    using EnergyTrading.Mdm.ServiceHost.Unity.Configuration;

    using Microsoft.Practices.Unity;

    using NUnit.Framework;

    public abstract class EntityMappingFixture<TEntity, TMapping> : Fixture
        where TEntity : IEntity, new()
        where TMapping : EntityMapping, new()
    {
        public readonly TimeSpan Interval = new TimeSpan(0, 0, 0, 1);

        [Test]
        public void ConvertToNexusId()
        {
            var container = new UnityContainer();

            // Self-register and set up service location 
            //container.RegisterInstance<IUnityContainer>(container);
            var locator = new UnityServiceLocator(container);

            var task = new SimpleMappingEngineConfiguration(container);
            task.Configure();

            var start = new DateTime(2000, 12, 31);
            var expected = new EnergyTrading.Mdm.Contracts.MdmId
            {
                MappingId = 34,
                SystemName = "Test",
                Identifier = "1",
                SourceSystemOriginated = true,
                StartDate = start,
                EndDate = DateUtility.MaxDate
            };
            var s1 = new SourceSystem { Name = "Test" };
            var m1 = new TMapping
            {
                Id = 34,
                System = s1,
                MappingValue = "1",
                IsMaster = true,
                Validity = new DateRange(start, DateUtility.MaxDate)
            };

            var mappingEngine = container.Resolve<IMappingEngine>();

            var candidate = mappingEngine.Map<IEntityMapping, EnergyTrading.Mdm.Contracts.MdmId>(m1);
            Check(expected, candidate);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNull()
        {
            var entity = new TEntity();

            entity.ProcessMapping(null);
        }

        [Test]
        public void AddFirst()
        {
            var entity = new TEntity();
            var system = new SourceSystem { Name = "Test" };
            var mapping = new TMapping { System = system, MappingValue = "1", Validity = DateRange.MaxDateRange };

            ProcessMapping(entity, mapping);

            Assert.AreEqual(1, entity.Mappings.Count, "Count differs");
        }

        [Test]
        public void AddTwoDifferentSystems()
        {
            var entity = new TEntity();

            var s1 = new SourceSystem { Name = "Test" };
            var m1 = new TMapping { System = s1, MappingValue = "1", Validity = DateRange.MaxDateRange };
            var s2 = new SourceSystem { Name = "Bob" };
            var m2 = new TMapping { System = s2, MappingValue = "1", Validity = DateRange.MaxDateRange };

            ProcessMapping(entity, m1);
            ProcessMapping(entity, m2);

            Assert.AreEqual(2, entity.Mappings.Count, "Count differs");
        }

        [Test]
        public void AddTwoDifferentValidity()
        {
            var start = new DateTime(2000, 12, 31);
            var start2 = new DateTime(2010, 1, 1);
            var entity = new TEntity();

            var s1 = new SourceSystem { Name = "Test" };
            var m1 = new TMapping { System = s1, MappingValue = "1", Validity = new DateRange(start, start.AddDays(3)) };
            var m2 = new TMapping { System = s1, MappingValue = "1", Validity = new DateRange(start2, DateUtility.MaxDate) };

            ProcessMapping(entity, m1);
            ProcessMapping(entity, m2);

            Assert.AreEqual(2, entity.Mappings.Count, "Count differs");
            Assert.AreEqual(start2.Add(-Interval), m1.Validity.Finish, "Finish differs");
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddTwoOverlapValidity()
        {
            var start = new DateTime(2000, 12, 31);
            var start2 = new DateTime(2010, 1, 1);
            var entity = new TEntity();

            var s1 = new SourceSystem { Name = "Test" };
            var m1 = new TMapping { System = s1, MappingValue = "1", Validity = new DateRange(start, DateUtility.MaxDate) };
            var m2 = new TMapping { System = s1, MappingValue = "1", Validity = new DateRange(start2, DateUtility.MaxDate) };
            var m3 = new TMapping { System = s1, MappingValue = "1", Validity = new DateRange(start.AddDays(2), DateUtility.MaxDate) };

            ProcessMapping(entity, m1);
            ProcessMapping(entity, m2);
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

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AttemptUpdateNonExistentMapping()
        {
            //var entity = new TEntity { Id = 12, Name = "Commodity" };
            var entity = new TEntity();
            var mapping = new TMapping { Id = 34 };

            ProcessMapping(entity, mapping);
        }

        [Test]
        public void UpdateMappingAdjustsEndDate()
        {
            var entity = new TEntity();
            var start = new DateTime(2000, 12, 31);
            var finish = DateUtility.Round(SystemTime.UtcNow().AddDays(5));

            var s1 = new SourceSystem { Name = "Test" };
            var m1 = new TMapping { Id = 12, System = s1, MappingValue = "1", Validity = new DateRange(start, DateUtility.MaxDate) };
            var m2 = new TMapping { Id = 12, System = s1, MappingValue = "1", Validity = new DateRange(start, finish) };

            // NB We deliberately bypasses the business logic
            AssignEntity(m1, entity);
            AddMapping(entity, m1);
            ProcessMapping(entity, m2);

            Assert.AreEqual(finish, m1.Validity.Finish, "Finish differs");
        }

        [Test]
        public void ChangeEndDateConstrainedToEntityValidity()
        {
            var start = new DateTime(2000, 12, 31);
            var finish = DateUtility.Round(SystemTime.UtcNow().AddDays(5));
            var entity = new TEntity();
            this.AssignValidity(entity, new DateRange(start, finish));

            var s1 = new SourceSystem { Name = "Test" };
            var m1 = new TMapping { System = s1, MappingValue = "1", Validity = new DateRange(start, DateUtility.MaxDate) };

            ProcessMapping(entity, m1);

            m1.ChangeEndDate(finish.AddDays(5));

            Assert.AreEqual(finish, m1.Validity.Finish, "Finish differs");
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateIncompatibleMapping()
        {
            var entity = new TEntity();
            var start = new DateTime(2000, 12, 31);
            var finish = DateUtility.Round(SystemTime.UtcNow().AddDays(5));

            // NB We deliberately bypasses the business logic

            var s1 = new SourceSystem { Name = "Test" };
            var m1 = new TMapping { Id = 12, System = s1, MappingValue = "1", Validity = new DateRange(start, DateUtility.MaxDate) };
            var m2 = new TMapping { Id = 12, System = s1, MappingValue = "2", Validity = new DateRange(start, finish) };
            AssignEntity(m1, entity);

            AddMapping(entity, m1);
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

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void AddMappingToExpiredEntityNotAllowed()
        {
            var start = new DateTime(2000, 12, 31);
            var finish = DateUtility.Round(SystemTime.UtcNow().AddDays(-1));
            var entity = new TEntity();
            AssignValidity(entity, new DateRange(start, finish));

            var s1 = new SourceSystem { Name = "Test" };
            var m1 = new TMapping { System = s1, MappingValue = "1", Validity = new DateRange(start, DateUtility.MaxDate) };

            try
            {
                ProcessMapping(entity, m1);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("Cannot change mapping for expired entity"));
                throw;
            }
        }

        protected abstract void AddMapping(TEntity entity, TMapping mapping);

        protected abstract void AssignEntity(TMapping mapping, TEntity entity);

        protected abstract void AssignValidity(TEntity entity, DateRange range);

        protected abstract void ProcessMapping(TEntity entity, TMapping mapping);
    }
}
