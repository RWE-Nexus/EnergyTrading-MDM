namespace EnergyTrading.MDM.Test.Contracts.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Data;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.MDM.Messages;

    [TestClass]
    public class PartyRoleCreateMappingdNoOverlappingRuleFixture
    {
        [TestMethod]
        public void NoEntityWithGivenIdentifierShouldFail()
        {
            // Assert
            var start = new DateTime(1999, 1, 1);
            var finish = new DateTime(2020, 12, 31);
            var validity = new DateRange(start, finish);
            var system = new SourceSystem { Name = "Test" };
            var expected = new PartyRoleMapping { System = system, MappingValue = "1", Validity = validity };
            var list = new List<PartyRoleMapping> { expected };
            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Queryable<PartyRoleMapping>()).Returns(list.AsQueryable());

            var identifier = new RWEST.Nexus.MDM.Contracts.NexusId
            {
                SystemName = "Test",
                Identifier = "1",
                StartDate = start.AddHours(-10),
                EndDate = start.AddHours(-5)
            };

            var request = new CreateMappingRequest() { EntityId = 1, Mapping = identifier};
            repository.Setup(x => x.FindOne<PartyRole>(It.IsAny<int>())).Returns((PartyRole)null);
            var rule = new PartyRoleCreateMappingdNoOverlappingRule<PartyRole, PartyRoleMapping>(repository.Object);

            // Act
            var result = rule.IsValid(request);

            // Assert
            repository.Verify(x => x.FindOne<PartyRole>(It.IsAny<int>()));
            Assert.IsFalse(result, "Rule failed");
        }

        [TestMethod]
        public void OverlappingIndentifierShouldFail()
        {
            // Assert
            var start = new DateTime(1999, 1, 1);
            var finish = new DateTime(2020, 12, 31);
            var validity = new DateRange(start, finish);
            var system = new SourceSystem { Name = "Test" };
            var expectedPartyRole = new PartyRole() { PartyRoleType = "PartyRole" };
            var expected = new PartyRoleMapping { System = system, MappingValue = "1", Validity = validity, PartyRole = expectedPartyRole};
            var list = new List<PartyRoleMapping> { expected };
            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Queryable<PartyRoleMapping>()).Returns(list.AsQueryable());
            repository.Setup(x => x.FindOne<PartyRole>(It.IsAny<int>())).Returns(expectedPartyRole);

            var identifier = new RWEST.Nexus.MDM.Contracts.NexusId
            {
                SystemName = "Test",
                Identifier = "1",
                StartDate = start.AddHours(5),
                EndDate = start.AddHours(10)
            };

            var request = new CreateMappingRequest() { EntityId = 1, Mapping = identifier };
            var rule = new PartyRoleCreateMappingdNoOverlappingRule<PartyRole, PartyRoleMapping>(repository.Object);

            // Act
            var result = rule.IsValid(request);

            // Assert
            repository.Verify(x => x.FindOne<PartyRole>(It.IsAny<int>()));
            Assert.IsFalse(result, "Rule failed");
        }

        [TestMethod]
        public void NoOverlappingIndentifierShouldPass()
        {
            // Assert
            var start = new DateTime(1999, 1, 1);
            var finish = new DateTime(2020, 12, 31);
            var validity = new DateRange(start, finish);
            var system = new SourceSystem { Name = "Test" };
            var expectedPartyRole = new PartyRole() { PartyRoleType = "PartyRole" };
            var expected = new PartyRoleMapping { System = system, MappingValue = "1", Validity = validity, PartyRole = expectedPartyRole };
            var list = new List<PartyRoleMapping> { expected };
            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Queryable<PartyRoleMapping>()).Returns(list.AsQueryable());
            repository.Setup(x => x.FindOne<PartyRole>(It.IsAny<int>())).Returns(expectedPartyRole);

            var identifier = new RWEST.Nexus.MDM.Contracts.NexusId
            {
                SystemName = "Test",
                Identifier = "1",
                StartDate = start.AddHours(-10),
                EndDate = start.AddHours(-5)
            };

            var request = new CreateMappingRequest() { EntityId = 1, Mapping = identifier };
            var rule = new PartyRoleCreateMappingdNoOverlappingRule<PartyRole, PartyRoleMapping>(repository.Object);

            // Act
            var result = rule.IsValid(request);

            // Assert
            repository.Verify(x => x.FindOne<PartyRole>(It.IsAny<int>()));
            Assert.IsTrue(result, "Rule failed");
        }
    }
}