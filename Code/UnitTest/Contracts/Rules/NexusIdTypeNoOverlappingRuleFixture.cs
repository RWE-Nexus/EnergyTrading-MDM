namespace EnergyTrading.Mdm.Test.Contracts.Rules
{
    using System;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Contracts.Rules;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class NexusIdNoOverlappingRuleFixture
    {
        [Test]
        public void NoOverlapPasses()
        {
            // Assert
            var start = new DateTime(1999, 1, 1);
            var finish = new DateTime(2020, 12, 31);
            var validity = new DateRange(start, finish);
            var system = new SourceSystem { Name = "Test" };
            var expected = new SourceSystemMapping { System = system, MappingValue = "1", Validity = validity };
            var list = new System.Collections.Generic.List<SourceSystemMapping> { expected };
            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Queryable<SourceSystemMapping>()).Returns(list.AsQueryable());

            var identifier = new EnergyTrading.Mdm.Contracts.MdmId
                {
                    SystemName = "Test", 
                    Identifier = "1", 
                    StartDate = start.AddHours(-10), 
                    EndDate = start.AddHours(-5)
                };

            var rule = new NexusIdNoOverlappingRule<SourceSystemMapping>(repository.Object);

            // Act
            var result = rule.IsValid(identifier);

            // Assert
            repository.Verify(x => x.Queryable<SourceSystemMapping>());
            Assert.IsTrue(result, "Rule failed");
        }

        [Test]
        public void OverlappingIdentifierFails()
        {
            // Assert
            var start = new DateTime(1999, 1, 1);
            var finish = new DateTime(2020, 12, 31);
            var validity = new DateRange(start, finish);
            var system = new SourceSystem { Name = "Test" };
            var expected = new SourceSystemMapping { System = system, MappingValue = "1", Validity = validity };
            var list = new System.Collections.Generic.List<SourceSystemMapping> { expected };
            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Queryable<SourceSystemMapping>()).Returns(list.AsQueryable());

            var identifier = new EnergyTrading.Mdm.Contracts.MdmId
            {
                SystemName = "Test",
                Identifier = "1",
                StartDate = start.AddHours(5),
                EndDate = start.AddHours(10)
            };

            var rule = new NexusIdNoOverlappingRule<SourceSystemMapping>(repository.Object);

            // Act
            var result = rule.IsValid(identifier);

            // Assert
            repository.Verify(x => x.Queryable<SourceSystemMapping>());
            Assert.IsFalse(result, "Rule failed");
        }
    }
}