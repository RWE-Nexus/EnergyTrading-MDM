namespace EnergyTrading.MDM.Test.Contracts.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading.MDM.ServiceHost.Unity.Configuration;

    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.MDM.Configuration;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.Validation;
    using RWEST.Nexus.MDM;

    using Party = RWEST.Nexus.MDM.Contracts.Party;

    [TestClass]
    public class PartyValidatorFixture : Fixture
    {
        [TestMethod]
        public void ValidatorResolution()
        {
            var container = CreateContainer();
            var meConfig = new SimpleMappingEngineConfiguration(container);
            meConfig.Configure();
            container.RegisterInstance<IRepository>(new Mock<IRepository>().Object);

            var config = new PartyConfiguration(container);
            config.Configure();

            var validator = container.Resolve<IValidator<Party>>("party");

            // Assert
            Assert.IsNotNull(validator, "Validator resolution failed");
        }

        [TestMethod]
        public void ValidPartyPasses()
        {
            // Assert
            var start = new DateTime(1999, 1, 1);
            var system = new SourceSystem { Name = "Test" };

            var systemList = new List<SourceSystem> { system };
            var systemRepository = new Mock<IRepository<SourceSystem>>();
            systemRepository.Setup(x => x.Queryable()).Returns(systemList.AsQueryable());

            var identifier = new RWEST.Nexus.MDM.Contracts.NexusId
            {
                SystemName = "Test",
                Identifier = "1",
                StartDate = start.AddHours(-10),
                EndDate = start.AddHours(-5)
            };

            var validatorEngine = new Mock<IValidatorEngine>();
            var validator = new PartyValidator(validatorEngine.Object, null);

            var party = new Party { Details = new RWEST.Nexus.MDM.Contracts.PartyDetails{Name = "Test"}, Identifiers = new RWEST.Nexus.MDM.Contracts.NexusIdList { identifier } };

            // Act
            var violations = new List<IRule>();
            var result = validator.IsValid(party, violations);

            // Assert
            Assert.IsTrue(result, "Validator failed");
            Assert.AreEqual(0, violations.Count, "Violation count differs");
        }

        [TestMethod]
        public void OverlapsRangeFails()
        {
            // Assert
            var start = new DateTime(1999, 1, 1);
            var finish = new DateTime(2020, 12, 31);
            var validity = new DateRange(start, finish);
            var system = new SourceSystem { Name = "Test" };
            var partyMapping = new PartyMapping { System = system, MappingValue = "1", Validity = validity };

            var list = new List<PartyMapping> { partyMapping };
            var partyMappingRepository = new Mock<IRepository>();
            partyMappingRepository.Setup(x => x.Queryable<PartyMapping>()).Returns(list.AsQueryable());

            var systemList = new List<SourceSystem>();
            var systemRepository = new Mock<IRepository<SourceSystem>>();
            systemRepository.Setup(x => x.Queryable()).Returns(systemList.AsQueryable());

            var overlapsRangeIdentifier = new RWEST.Nexus.MDM.Contracts.NexusId
            {
                SystemName = "Test",
                Identifier = "1",
                StartDate = start.AddHours(10),
                EndDate = start.AddHours(15)
            };

            var identifierValidator = new NexusIdValidator<PartyMapping>(partyMappingRepository.Object);
            var validatorEngine = new Mock<IValidatorEngine>();
            validatorEngine.Setup(x => x.IsValid(It.IsAny<RWEST.Nexus.MDM.Contracts.NexusId>(), It.IsAny<IList<IRule>>()))
                          .Returns((RWEST.Nexus.MDM.Contracts.NexusId x, IList<IRule> y) => identifierValidator.IsValid(x, y));
            var validator = new PartyValidator(validatorEngine.Object, null);

            var party = new Party { Identifiers = new RWEST.Nexus.MDM.Contracts.NexusIdList { overlapsRangeIdentifier } };

            // Act
            var violations = new List<IRule>();
            var result = validator.IsValid(party, violations);

            // Assert
            Assert.IsFalse(result, "Validator succeeded");
        }

        [TestMethod]
        public void BadSystemFails()
        {
            // Assert
            var start = new DateTime(1999, 1, 1);
            var finish = new DateTime(2020, 12, 31);
            var validity = new DateRange(start, finish);
            var system = new SourceSystem { Name = "Test" };
            var partyMapping = new PartyMapping { System = system, MappingValue = "1", Validity = validity };

            var list = new List<PartyMapping> { partyMapping };
            var partyMappingRepository = new Mock<IRepository>();
            partyMappingRepository.Setup(x => x.Queryable<PartyMapping>()).Returns(list.AsQueryable());

            var badSystemIdentifier = new RWEST.Nexus.MDM.Contracts.NexusId
            {
                SystemName = "Jim",
                Identifier = "1",
                StartDate = start.AddHours(-10),
                EndDate = start.AddHours(-5)
            };

            var identifierValidator = new NexusIdValidator<PartyMapping>(partyMappingRepository.Object);
            var validatorEngine = new Mock<IValidatorEngine>();
            validatorEngine.Setup(x => x.IsValid(It.IsAny<RWEST.Nexus.MDM.Contracts.NexusId>(), It.IsAny<IList<IRule>>()))
                           .Returns((RWEST.Nexus.MDM.Contracts.NexusId x, IList<IRule> y) => identifierValidator.IsValid(x, y));
            var validator = new PartyValidator(validatorEngine.Object, null);

            var party = new Party { Identifiers = new RWEST.Nexus.MDM.Contracts.NexusIdList { badSystemIdentifier } };

            // Act
            var violations = new List<IRule>();
            var result = validator.IsValid(party, violations);

            // Assert
            Assert.IsFalse(result, "Validator succeeded");
        }    
    }
}
