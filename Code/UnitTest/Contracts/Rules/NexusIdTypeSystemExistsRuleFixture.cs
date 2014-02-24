namespace EnergyTrading.MDM.Test.Contracts.Rules
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Rules;

    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    [TestClass]
    public class NexusIdSystemExistsRuleFixture
    {
        [TestMethod]
        public void SystemPresentPasses()
        {
            // Arrange
            var system = new SourceSystem { Name = "Test" };
            var systemList = new System.Collections.Generic.List<SourceSystem> { system };
            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Queryable<SourceSystem>()).Returns(systemList.AsQueryable());

            var identifier = new NexusId { SystemName = "Test", Identifier = "1" };

            var rule = new NexusIdSystemExistsRule(repository.Object);

            // Act
            var result = rule.IsValid(identifier);

            // Assert
            repository.Verify(x => x.Queryable<SourceSystem>());
            Assert.IsTrue(result, "Rule failed");
        }

        [TestMethod]
        public void SystemMissingFails()
        {
            // Assert
            var systemList = new System.Collections.Generic.List<SourceSystem>();
            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Queryable<SourceSystem>()).Returns(systemList.AsQueryable());

            var identifier = new NexusId { SystemName = "Test", Identifier = "1" };

            var rule = new NexusIdSystemExistsRule(repository.Object);

            // Act
            var result = rule.IsValid(identifier);

            // Assert
            repository.Verify(x => x.Queryable<SourceSystem>());
            Assert.IsFalse(result, "Rule failed");            
        }
    }
}
