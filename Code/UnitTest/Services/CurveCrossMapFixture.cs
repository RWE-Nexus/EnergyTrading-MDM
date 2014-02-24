namespace RWEST.Nexus.MDM.Test.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using RWEST.Nexus.MDM.Contracts;
    using RWEST.Nexus;
    using RWEST.Nexus.Data;
    using RWEST.Nexus.Mapping;
    using RWEST.Nexus.Validation;
    using RWEST.Nexus.Search;
    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Messages;
    using RWEST.Nexus.MDM.Services;

    using SourceSystem = RWEST.Nexus.MDM.SourceSystem;
	
    [TestClass]
    public class CurveCrossMapFixture
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullRequestErrors()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            var service = new CurveService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // Act
            service.CrossMap(null);
        }

        [TestMethod]
        public void UnsuccessfulMatchReturnsNotFound()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            var service = new CurveService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var list = new List<CurveMapping>();
            repository.Setup(x => x.Queryable<CurveMapping>()).Returns(list.AsQueryable());

            var request = new CrossMappingRequest { SystemName = "Endur", Identifier = "A", ValidAt = SystemTime.UtcNow(), TargetSystemName = "Trayport" };

            // Act
            var contract = service.CrossMap(request);

            // Assert
            Assert.IsNotNull(contract, "Contract null");
            Assert.IsFalse(contract.IsValid, "Contract valid");
            Assert.AreEqual(ErrorType.NotFound, contract.Error.Type, "ErrorType difers");
        }

        [TestMethod]
        public void SuccessMatch()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            var service = new CurveService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // Domain details
            var system = new MDM.SourceSystem { Name = "Endur" };
            var mapping = new CurveMapping
            {
                System = system,
                MappingValue = "A"
            };
            var targetSystem = new MDM.SourceSystem { Name = "Trayport" };
            var targetMapping = new CurveMapping
            {
                System = targetSystem,
                MappingValue = "B",
                IsDefault = true
            };
            var curve = new MDM.Curve
            {
                Id = 1,
                Timestamp = new byte[] { 1, 0, 0, 0, 0, 0, 0, 0 }
            };
            //Curve.AddDetails(details);
            curve.ProcessMapping(mapping);
            curve.ProcessMapping(targetMapping);

            // Contract details
            var targetIdentifier = new NexusId
            {
                SystemName = "Trayport",
                Identifier = "B"
            };

            mappingEngine.Setup(x => x.Map<CurveMapping, NexusId>(targetMapping)).Returns(targetIdentifier);

            var list = new List<CurveMapping> { mapping };
            repository.Setup(x => x.Queryable<CurveMapping>()).Returns(list.AsQueryable());

            var request = new CrossMappingRequest
            {
                SystemName = "Endur", 
                Identifier = "A", 
                TargetSystemName = "trayport",
                ValidAt = SystemTime.UtcNow(),
                Version = 0
            };

            // Act
            var response = service.CrossMap(request);
            var candidate = response.Contract;

            // Assert
            Assert.IsNotNull(response, "Contract null");
            Assert.IsNotNull(candidate, "Mapping null");
            Assert.AreEqual(1, candidate.Mappings.Count, "Identifier count incorrect");
            Assert.AreSame(targetIdentifier, candidate.Mappings[0], "Different identifier assigned");
        }

        [TestMethod]
        public void SuccessMatchSameVersion()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            var service = new CurveService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // Domain details
            var system = new MDM.SourceSystem { Name = "Endur" };
            var mapping = new CurveMapping
            {
                System = system,
                MappingValue = "A"
            };
            var targetSystem = new MDM.SourceSystem { Name = "Trayport" };
            var targetMapping = new CurveMapping
            {
                System = targetSystem,
                MappingValue = "B",
                IsDefault = true
            };
            var curve = new MDM.Curve
            {
                Id = 1,
            };
            //Curve.AddDetails(details);
            curve.ProcessMapping(mapping);
            curve.ProcessMapping(targetMapping);

            // Contract details
            var targetIdentifier = new NexusId
            {
                SystemName = "Trayport",
                Identifier = "B"
            };

            mappingEngine.Setup(x => x.Map<CurveMapping, NexusId>(targetMapping)).Returns(targetIdentifier);

            var list = new List<CurveMapping> { mapping };
            repository.Setup(x => x.Queryable<CurveMapping>()).Returns(list.AsQueryable());

            var request = new CrossMappingRequest
            {
                SystemName = "Endur", 
                Identifier = "A", 
                TargetSystemName = "trayport",
                ValidAt = SystemTime.UtcNow(),
                Version = 0
            };

            // Act
            var response = service.CrossMap(request);
            var candidate = response.Contract;

            // Assert
            Assert.IsNotNull(response, "Contract null");
            Assert.IsTrue(response.IsValid, "Contract not valid");
            Assert.IsNull(candidate, "Mapping null");
        }
    }
}
