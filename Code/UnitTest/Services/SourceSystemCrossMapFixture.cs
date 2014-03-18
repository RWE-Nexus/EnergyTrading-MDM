namespace EnergyTrading.MDM.Test.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading.Mdm.Contracts;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;
    using EnergyTrading.Search;
    using EnergyTrading.Mdm;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.MDM.Services;

    using SourceSystem = EnergyTrading.MDM.SourceSystem;
	
    [TestClass]
    public class SourceSystemCrossMapFixture
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

            var service = new SourceSystemService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

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

            var service = new SourceSystemService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var list = new List<SourceSystemMapping>();
            repository.Setup(x => x.Queryable<SourceSystemMapping>()).Returns(list.AsQueryable());

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

            var service = new SourceSystemService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // Domain details
            var system = new MDM.SourceSystem { Name = "Endur" };
            var mapping = new SourceSystemMapping
            {
                System = system,
                MappingValue = "A"
            };
            var targetSystem = new MDM.SourceSystem { Name = "Trayport" };
            var targetMapping = new SourceSystemMapping
            {
                System = targetSystem,
                MappingValue = "B",
                IsDefault = true
            };
            var sourcesystem = new MDM.SourceSystem
            {
                Id = 1,
                Timestamp = new byte[] { 1, 0, 0, 0, 0, 0, 0, 0 }
            };
            //SourceSystem.AddDetails(details);
            sourcesystem.ProcessMapping(mapping);
            sourcesystem.ProcessMapping(targetMapping);

            // Contract details
            var targetIdentifier = new MdmId
            {
                SystemName = "Trayport",
                Identifier = "B"
            };

            mappingEngine.Setup(x => x.Map<SourceSystemMapping, MdmId>(targetMapping)).Returns(targetIdentifier);

            var list = new List<SourceSystemMapping> { mapping };
            repository.Setup(x => x.Queryable<SourceSystemMapping>()).Returns(list.AsQueryable());

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

            var service = new SourceSystemService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // Domain details
            var system = new MDM.SourceSystem { Name = "Endur" };
            var mapping = new SourceSystemMapping
            {
                System = system,
                MappingValue = "A"
            };
            var targetSystem = new MDM.SourceSystem { Name = "Trayport" };
            var targetMapping = new SourceSystemMapping
            {
                System = targetSystem,
                MappingValue = "B",
                IsDefault = true
            };
            var sourcesystem = new MDM.SourceSystem
            {
                Id = 1,
            };
            //SourceSystem.AddDetails(details);
            sourcesystem.ProcessMapping(mapping);
            sourcesystem.ProcessMapping(targetMapping);

            // Contract details
            var targetIdentifier = new MdmId
            {
                SystemName = "Trayport",
                Identifier = "B"
            };

            mappingEngine.Setup(x => x.Map<SourceSystemMapping, MdmId>(targetMapping)).Returns(targetIdentifier);

            var list = new List<SourceSystemMapping> { mapping };
            repository.Setup(x => x.Queryable<SourceSystemMapping>()).Returns(list.AsQueryable());

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
