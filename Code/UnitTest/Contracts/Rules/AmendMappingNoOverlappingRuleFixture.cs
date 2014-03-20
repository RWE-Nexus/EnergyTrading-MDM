namespace EnergyTrading.MDM.Test.Contracts.Rules
{
    using System;
    using System.Linq;

    using EnergyTrading;

    using NUnit.Framework;

    using Moq;

    using EnergyTrading.Data;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.MDM.Messages;

    [TestFixture]
    public class AmendMappingNoOverlappingRuleFixture
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

            var request = new AmendMappingRequest(){EntityId = 1, Mapping = identifier, MappingId = 1};

            var rule = new AmendMappingNoOverlappingRule<SourceSystemMapping>(repository.Object);

            // Act
            var result = rule.IsValid(request);

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

            var request = new AmendMappingRequest() { EntityId = 1, Mapping = identifier, MappingId = 1 };

            var rule = new AmendMappingNoOverlappingRule<SourceSystemMapping>(repository.Object);

            // Act
            var result = rule.IsValid(request);

            // Assert
            repository.Verify(x => x.Queryable<SourceSystemMapping>());
            Assert.IsFalse(result, "Rule failed");
        } 
    }
}