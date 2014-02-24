namespace EnergyTrading.MDM.Test.Contracts.Rules
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading;
    using EnergyTrading.Data;
    using RWEST.Nexus.MDM;

    [TestClass]
    public class NexusIdNoOverlappingRuleFixture
    {
        [TestMethod]
        public void NoOverlapPasses()
        {
            // Assert
            var start = new DateTime(1999, 1, 1);
            var finish = new DateTime(2020, 12, 31);
            var validity = new DateRange(start, finish);
            var system = new SourceSystem { Name = "Test" };
            var expected = new PersonMapping { System = system, MappingValue = "1", Validity = validity };
            var list = new System.Collections.Generic.List<PersonMapping> { expected };
            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Queryable<PersonMapping>()).Returns(list.AsQueryable());

            var identifier = new RWEST.Nexus.MDM.Contracts.NexusId
                {
                    SystemName = "Test", 
                    Identifier = "1", 
                    StartDate = start.AddHours(-10), 
                    EndDate = start.AddHours(-5)
                };

            var rule = new NexusIdNoOverlappingRule<PersonMapping>(repository.Object);

            // Act
            var result = rule.IsValid(identifier);

            // Assert
            repository.Verify(x => x.Queryable<PersonMapping>());
            Assert.IsTrue(result, "Rule failed");
        }

        [TestMethod]
        public void OverlappingIdentifierFails()
        {
            // Assert
            var start = new DateTime(1999, 1, 1);
            var finish = new DateTime(2020, 12, 31);
            var validity = new DateRange(start, finish);
            var system = new SourceSystem { Name = "Test" };
            var expected = new PersonMapping { System = system, MappingValue = "1", Validity = validity };
            var list = new System.Collections.Generic.List<PersonMapping> { expected };
            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Queryable<PersonMapping>()).Returns(list.AsQueryable());

            var identifier = new RWEST.Nexus.MDM.Contracts.NexusId
            {
                SystemName = "Test",
                Identifier = "1",
                StartDate = start.AddHours(5),
                EndDate = start.AddHours(10)
            };

            var rule = new NexusIdNoOverlappingRule<PersonMapping>(repository.Object);

            // Act
            var result = rule.IsValid(identifier);

            // Assert
            repository.Verify(x => x.Queryable<PersonMapping>());
            Assert.IsFalse(result, "Rule failed");
        }
    }
}