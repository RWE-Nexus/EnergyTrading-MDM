﻿namespace EnergyTrading.MDM.Test.Contracts.Rules
{
    using System.Linq;

    using EnergyTrading.Data;
    using EnergyTrading.MDM.Contracts.Rules;

    using Moq;

    using NUnit.Framework;

    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    [TestFixture]
    public class NexusIdSystemExistsRuleFixture
    {
        [Test]
        public void SystemPresentPasses()
        {
            // Arrange
            var system = new SourceSystem { Name = "Test" };
            var systemList = new System.Collections.Generic.List<SourceSystem> { system };
            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Queryable<SourceSystem>()).Returns(systemList.AsQueryable());

            var identifier = new EnergyTrading.Mdm.Contracts.MdmId { SystemName = "Test", Identifier = "1" };

            var rule = new MdmIdSystemExistsRule(repository.Object);

            // Act
            var result = rule.IsValid(identifier);

            // Assert
            repository.Verify(x => x.Queryable<SourceSystem>());
            Assert.IsTrue(result, "Rule failed");
        }

        [Test]
        public void SystemMissingFails()
        {
            // Assert
            var systemList = new System.Collections.Generic.List<SourceSystem>();
            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Queryable<SourceSystem>()).Returns(systemList.AsQueryable());

            var identifier = new EnergyTrading.Mdm.Contracts.MdmId { SystemName = "Test", Identifier = "1" };

            var rule = new MdmIdSystemExistsRule(repository.Object);

            // Act
            var result = rule.IsValid(identifier);

            // Assert
            repository.Verify(x => x.Queryable<SourceSystem>());
            Assert.IsFalse(result, "Rule failed");            
        }
    }
}
