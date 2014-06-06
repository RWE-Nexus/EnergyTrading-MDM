namespace EnergyTrading.Mdm.Test.Contracts.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Mdm.Contracts.Validators;
    using EnergyTrading.Mdm.ServiceHost.Unity.Configuration;
    using EnergyTrading.Validation;

    using Microsoft.Practices.Unity;

    using Moq;

    using NUnit.Framework;

    using DateRange = EnergyTrading.DateRange;
    using SourceSystem = EnergyTrading.Mdm.Contracts.SourceSystem;

    [TestFixture]
    public partial class SourceSystemValidatorFixture : Fixture
    {
        [Test]
        public void ValidatorResolution()
        {
            var container = CreateContainer();
            var meConfig = new SimpleMappingEngineConfiguration(container);
            meConfig.Configure();

            var repository = new Mock<IRepository>();
            container.RegisterInstance(repository.Object);

            var config = new SourceSystemConfiguration(container);
            config.Configure();

            var validator = container.Resolve<IValidator<SourceSystem>>("sourcesystem");

            // Assert
            Assert.IsNotNull(validator, "Validator resolution failed");
        }

        [Test]
        public void ValidSourceSystemPasses()
        {
            // Assert
            var start = new DateTime(1999, 1, 1);
            var system = new Mdm.SourceSystem { Name = "Test" };

            var systemList = new List<Mdm.SourceSystem> { system };
            var systemRepository = new Mock<IRepository>();
            var repository = new StubValidatorRepository();

            systemRepository.Setup(x => x.Queryable<Mdm.SourceSystem>()).Returns(systemList.AsQueryable());

            var identifier = new EnergyTrading.Mdm.Contracts.MdmId
            {
                SystemName = "Test",
                Identifier = "1",
                StartDate = start.AddHours(-10),
                EndDate = start.AddHours(-5)
            };

            var validatorEngine = new Mock<IValidatorEngine>();
            var validator = new SourceSystemValidator(validatorEngine.Object, repository);

            var sourcesystem = new SourceSystem { Details = new EnergyTrading.Mdm.Contracts.SourceSystemDetails{Name = "Test"}, Identifiers = new EnergyTrading.Mdm.Contracts.MdmIdList { identifier } };
            this.AddRelatedEntities(sourcesystem);

            // Act
            var violations = new List<IRule>();
            var result = validator.IsValid(sourcesystem, violations);

            // Assert
            Assert.IsTrue(result, "Validator failed");
            Assert.AreEqual(0, violations.Count, "Violation count differs");
        }

        [Test]
        public void OverlapsRangeFails()
        {
            // Assert
            var start = new DateTime(1999, 1, 1);
            var finish = new DateTime(2020, 12, 31);
            var validity = new DateRange(start, finish);
            var system = new Mdm.SourceSystem { Name = "Test" };
            var sourcesystemMapping = new SourceSystemMapping { System = system, MappingValue = "1", Validity = validity };

            var list = new List<SourceSystemMapping> { sourcesystemMapping };
            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Queryable<SourceSystemMapping>()).Returns(list.AsQueryable());

            var systemList = new List<Mdm.SourceSystem>();
            var systemRepository = new Mock<IRepository>();
            systemRepository.Setup(x => x.Queryable<Mdm.SourceSystem>()).Returns(systemList.AsQueryable());

            var overlapsRangeIdentifier = new EnergyTrading.Mdm.Contracts.MdmId
            {
                SystemName = "Test",
                Identifier = "1",
                StartDate = start.AddHours(10),
                EndDate = start.AddHours(15)
            };

            var identifierValidator = new NexusIdValidator<SourceSystemMapping>(repository.Object);
            var validatorEngine = new Mock<IValidatorEngine>();
            validatorEngine.Setup(x => x.IsValid(It.IsAny<EnergyTrading.Mdm.Contracts.MdmId>(), It.IsAny<IList<IRule>>()))
                          .Returns((EnergyTrading.Mdm.Contracts.MdmId x, IList<IRule> y) => identifierValidator.IsValid(x, y));
            var validator = new SourceSystemValidator(validatorEngine.Object, repository.Object);

            var sourcesystem = new SourceSystem { Identifiers = new EnergyTrading.Mdm.Contracts.MdmIdList { overlapsRangeIdentifier } };

            // Act
            var violations = new List<IRule>();
            var result = validator.IsValid(sourcesystem, violations);

            // Assert
            Assert.IsFalse(result, "Validator succeeded");
        }

        [Test]
        public void BadSystemFails()
        {
            // Assert
            var start = new DateTime(1999, 1, 1);
            var finish = new DateTime(2020, 12, 31);
            var validity = new DateRange(start, finish);
            var system = new Mdm.SourceSystem { Name = "Test" };
            var sourcesystemMapping = new SourceSystemMapping { System = system, MappingValue = "1", Validity = validity };

            var list = new List<SourceSystemMapping> { sourcesystemMapping };
            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Queryable<SourceSystemMapping>()).Returns(list.AsQueryable());

            var badSystemIdentifier = new EnergyTrading.Mdm.Contracts.MdmId
            {
                SystemName = "Jim",
                Identifier = "1",
                StartDate = start.AddHours(-10),
                EndDate = start.AddHours(-5)
            };

            var identifierValidator = new NexusIdValidator<SourceSystemMapping>(repository.Object);
            var validatorEngine = new Mock<IValidatorEngine>();
            validatorEngine.Setup(x => x.IsValid(It.IsAny<EnergyTrading.Mdm.Contracts.MdmId>(), It.IsAny<IList<IRule>>()))
                           .Returns((EnergyTrading.Mdm.Contracts.MdmId x, IList<IRule> y) => identifierValidator.IsValid(x, y));
            var validator = new SourceSystemValidator(validatorEngine.Object, repository.Object);

            var sourcesystem = new SourceSystem { Identifiers = new EnergyTrading.Mdm.Contracts.MdmIdList { badSystemIdentifier } };

            // Act
            var violations = new List<IRule>();
            var result = validator.IsValid(sourcesystem, violations);

            // Assert
            Assert.IsFalse(result, "Validator succeeded");
        }

        [Test]
        public void ParentShouldNotBeSameAsEntity()
        {
            // Arrange
            var parentId = 999;
            var sourceSystem = NewSourceSystem("SameName", parentId);

            var repository = new Mock<IRepository>();
            repository.Setup(x => x.FindOne<Mdm.SourceSystem>(parentId))
                .Returns(new Mdm.SourceSystem { Name = "SameName"});

            // Act
            var violations = new List<IRule>();
            var validator = new SourceSystemValidator(new Mock<IValidatorEngine>().Object, repository.Object);
            var result = validator.IsValid(sourceSystem, violations);

            // Assert
            Assert.IsFalse(result, "Validation should not have succeeded");
            Assert.AreEqual(1, violations.Count);
            Assert.AreEqual("Parent must not be same as entity", violations[0].Message);
        }

        partial void AddRelatedEntities(SourceSystem contract);

        private SourceSystem NewSourceSystem(string name, int parentId)
        {
            var sourceSystem = new SourceSystem
            {
                Details =
                {
                    Name = name,
                    Parent = new EntityId
                    {
                        Identifier = new MdmId
                        {
                            SystemName = "Nexus",
                            Identifier = parentId.ToString(CultureInfo.InvariantCulture),
                            IsMdmId = true
                        }
                    }
                }
            };

            return sourceSystem;
        }
    }
}
