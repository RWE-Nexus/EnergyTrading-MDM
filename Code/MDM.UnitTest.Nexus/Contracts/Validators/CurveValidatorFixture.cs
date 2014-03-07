namespace RWEST.Nexus.MDM.Test.Contracts.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using RWEST.Nexus.MDM.Configuration;
    using RWEST.Nexus.MDM.Contracts.Validators;
    using RWEST.Nexus;
    using RWEST.Nexus.Data;
    using RWEST.Nexus.Validation;
    using RWEST.Nexus.MDM;

    using Curve = RWEST.Nexus.MDM.Contracts.Curve;

    [TestClass]
    public partial class CurveValidatorFixture : Fixture
    {
        [TestMethod]
        public void ValidatorResolution()
        {
            var container = CreateContainer();
            var meConfig = new SimpleMappingEngineConfiguration(container);
            meConfig.Configure();

			var repository = new Mock<IRepository>();
            container.RegisterInstance(repository.Object);

            var config = new CurveConfiguration(container);
            config.Configure();

            var validator = container.Resolve<IValidator<Curve>>("curve");

            // Assert
            Assert.IsNotNull(validator, "Validator resolution failed");
        }

        [TestMethod]
        public void ValidCurvePasses()
        {
            // Assert
            var start = new DateTime(1999, 1, 1);
            var system = new MDM.SourceSystem { Name = "Test" };

            var systemList = new List<MDM.SourceSystem> { system };
            var systemRepository = new Mock<IRepository<MDM.SourceSystem>>();
			var repository = new StubValidatorRepository();

            systemRepository.Setup(x => x.Queryable()).Returns(systemList.AsQueryable());

            var identifier = new MDM.Contracts.NexusId
            {
                SystemName = "Test",
                Identifier = "1",
                StartDate = start.AddHours(-10),
                EndDate = start.AddHours(-5)
            };

            var validatorEngine = new Mock<IValidatorEngine>();
            var validator = new CurveValidator(validatorEngine.Object, repository);

            var curve = new Curve { Identifiers = new MDM.Contracts.NexusIdList { identifier } };
            this.AddRelatedEntities(curve);

            // Act
            var violations = new List<IRule>();
            var result = validator.IsValid(curve, violations);

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
            var curveMapping = new CurveMapping { System = system, MappingValue = "1", Validity = validity };

            var list = new List<CurveMapping> { curveMapping };
            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Queryable<CurveMapping>()).Returns(list.AsQueryable());

            var systemList = new List<MDM.SourceSystem>();
            var systemRepository = new Mock<IRepository<MDM.SourceSystem>>();
            systemRepository.Setup(x => x.Queryable()).Returns(systemList.AsQueryable());

            var overlapsRangeIdentifier = new MDM.Contracts.NexusId
            {
                SystemName = "Test",
                Identifier = "1",
                StartDate = start.AddHours(10),
                EndDate = start.AddHours(15)
            };

            var identifierValidator = new NexusIdValidator<CurveMapping>(repository.Object);
            var validatorEngine = new Mock<IValidatorEngine>();
            validatorEngine.Setup(x => x.IsValid(It.IsAny<MDM.Contracts.NexusId>(), It.IsAny<IList<IRule>>()))
                          .Returns((MDM.Contracts.NexusId x, IList<IRule> y) => identifierValidator.IsValid(x, y));
            var validator = new CurveValidator(validatorEngine.Object, repository.Object);

            var curve = new Curve { Identifiers = new MDM.Contracts.NexusIdList { overlapsRangeIdentifier } };

            // Act
            var violations = new List<IRule>();
            var result = validator.IsValid(curve, violations);

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
            var curveMapping = new CurveMapping { System = system, MappingValue = "1", Validity = validity };

            var list = new List<CurveMapping> { curveMapping };
            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Queryable<CurveMapping>()).Returns(list.AsQueryable());

            var badSystemIdentifier = new MDM.Contracts.NexusId
            {
                SystemName = "Jim",
                Identifier = "1",
                StartDate = start.AddHours(-10),
                EndDate = start.AddHours(-5)
            };

            var identifierValidator = new NexusIdValidator<CurveMapping>(repository.Object);
            var validatorEngine = new Mock<IValidatorEngine>();
            validatorEngine.Setup(x => x.IsValid(It.IsAny<MDM.Contracts.NexusId>(), It.IsAny<IList<IRule>>()))
                           .Returns((MDM.Contracts.NexusId x, IList<IRule> y) => identifierValidator.IsValid(x, y));
            var validator = new CurveValidator(validatorEngine.Object, repository.Object);

            var curve = new Curve { Identifiers = new MDM.Contracts.NexusIdList { badSystemIdentifier } };

            // Act
            var violations = new List<IRule>();
            var result = validator.IsValid(curve, violations);

            // Assert
            Assert.IsFalse(result, "Validator succeeded");
		}
		
		partial void AddRelatedEntities(MDM.Contracts.Curve contract);

    }
}
