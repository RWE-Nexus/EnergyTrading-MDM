using RWEST.Nexus.MDM.Contracts;

namespace EnergyTrading.MDM.Test.Contracts.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading.MDM.ServiceHost.Unity.Configuration;

    using global::MDM.ServiceHost.Unity.Nexus.Configuration;

    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.Validation;
    using RWEST.Nexus.MDM;

    using Location = RWEST.Nexus.MDM.Contracts.Location;

    [TestClass]
    public partial class LocationValidatorFixture : Fixture
    {
        [TestMethod]
        public void ValidatorResolution()
        {
            var container = CreateContainer();
            var meConfig = new SimpleMappingEngineConfiguration(container);
            meConfig.Configure();

			var repository = new Mock<IRepository>();
            container.RegisterInstance(repository.Object);

            var config = new LocationConfiguration(container);
            config.Configure();

            var validator = container.Resolve<IValidator<Location>>("location");

            // Assert
            Assert.IsNotNull(validator, "Validator resolution failed");
        }

        [TestMethod]
        public void ValidLocationPasses()
        {
            // Assert
            var start = new DateTime(1999, 1, 1);
            var system = new MDM.SourceSystem { Name = "Test" };

            var systemList = new List<MDM.SourceSystem> { system };
            var systemRepository = new Mock<IRepository<MDM.SourceSystem>>();
			var repository = new StubValidatorRepository();

            systemRepository.Setup(x => x.Queryable()).Returns(systemList.AsQueryable());

            var identifier = new RWEST.Nexus.MDM.Contracts.NexusId
            {
                SystemName = "Test",
                Identifier = "1",
                StartDate = start.AddHours(-10),
                EndDate = start.AddHours(-5)
            };

            var validatorEngine = new Mock<IValidatorEngine>();
            var validator = new LocationValidator(validatorEngine.Object, repository);

            var location = new Location { Details = new RWEST.Nexus.MDM.Contracts.LocationDetails{Name = "Test"}, Identifiers = new RWEST.Nexus.MDM.Contracts.NexusIdList { identifier } };
            this.AddRelatedEntities(location);

            // Act
            var violations = new List<IRule>();
            var result = validator.IsValid(location, violations);

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
            var system = new MDM.SourceSystem { Name = "Test" };
            var locationMapping = new LocationMapping { System = system, MappingValue = "1", Validity = validity };

            var list = new List<LocationMapping> { locationMapping };
            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Queryable<LocationMapping>()).Returns(list.AsQueryable());

            var systemList = new List<MDM.SourceSystem>();
            var systemRepository = new Mock<IRepository<MDM.SourceSystem>>();
            systemRepository.Setup(x => x.Queryable()).Returns(systemList.AsQueryable());

            var overlapsRangeIdentifier = new RWEST.Nexus.MDM.Contracts.NexusId
            {
                SystemName = "Test",
                Identifier = "1",
                StartDate = start.AddHours(10),
                EndDate = start.AddHours(15)
            };

            var identifierValidator = new NexusIdValidator<LocationMapping>(repository.Object);
            var validatorEngine = new Mock<IValidatorEngine>();
            validatorEngine.Setup(x => x.IsValid(It.IsAny<RWEST.Nexus.MDM.Contracts.NexusId>(), It.IsAny<IList<IRule>>()))
                          .Returns((RWEST.Nexus.MDM.Contracts.NexusId x, IList<IRule> y) => identifierValidator.IsValid(x, y));
            var validator = new LocationValidator(validatorEngine.Object, repository.Object);

            var location = new Location { Identifiers = new RWEST.Nexus.MDM.Contracts.NexusIdList { overlapsRangeIdentifier } };

            // Act
            var violations = new List<IRule>();
            var result = validator.IsValid(location, violations);

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
            var system = new MDM.SourceSystem { Name = "Test" };
            var locationMapping = new LocationMapping { System = system, MappingValue = "1", Validity = validity };

            var list = new List<LocationMapping> { locationMapping };
            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Queryable<LocationMapping>()).Returns(list.AsQueryable());

            var badSystemIdentifier = new RWEST.Nexus.MDM.Contracts.NexusId
            {
                SystemName = "Jim",
                Identifier = "1",
                StartDate = start.AddHours(-10),
                EndDate = start.AddHours(-5)
            };

            var identifierValidator = new NexusIdValidator<LocationMapping>(repository.Object);
            var validatorEngine = new Mock<IValidatorEngine>();
            validatorEngine.Setup(x => x.IsValid(It.IsAny<RWEST.Nexus.MDM.Contracts.NexusId>(), It.IsAny<IList<IRule>>()))
                           .Returns((RWEST.Nexus.MDM.Contracts.NexusId x, IList<IRule> y) => identifierValidator.IsValid(x, y));
            var validator = new LocationValidator(validatorEngine.Object, repository.Object);

            var location = new Location { Identifiers = new RWEST.Nexus.MDM.Contracts.NexusIdList { badSystemIdentifier } };

            // Act
            var violations = new List<IRule>();
            var result = validator.IsValid(location, violations);

            // Assert
            Assert.IsFalse(result, "Validator succeeded");
		}

        [TestMethod]
        public void ParentShouldNotBeSameAsEntity()
        {
            // Arrange
            var parentId = 999;
            var location = NewLocation("SameName", parentId);

            var repository = new Mock<IRepository>();
            repository.Setup(x => x.FindOne<MDM.Location>(parentId))
                .Returns(new MDM.Location { Name = "SameName"});

            // Act
            var violations = new List<IRule>();
            var validator = new LocationValidator(new Mock<IValidatorEngine>().Object, repository.Object);
            var result = validator.IsValid(location, violations);

            // Assert
            Assert.IsFalse(result, "Validation should not have succeeded");
            Assert.AreEqual(1, violations.Count);
            Assert.AreEqual("Parent must not be same as entity", violations[0].Message);
        }
		
		partial void AddRelatedEntities(RWEST.Nexus.MDM.Contracts.Location contract);

        private Location NewLocation(string name, int parentId)
        {
            var location = new Location();
            location.Details.Name = name;
            location.Details.Parent = new EntityId
                {Identifier = new NexusId
                    {
                        SystemName = "Nexus", Identifier = parentId.ToString(), IsNexusId = true
                    }};
            return location;
        }
    }
}
