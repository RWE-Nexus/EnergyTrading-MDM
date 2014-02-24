namespace EnergyTrading.MDM.Test.Services
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.MDM.Services;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    [TestClass]
    public class AgreementCreateMappingFixture : Fixture
    {
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void NullContractInvalid()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();

            var service = new AgreementService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            validatorFactory.Setup(x => x.IsValid(It.IsAny<object>(), It.IsAny<IList<IRule>>())).Returns(false);

            // Act
            service.CreateMapping(null);
        }

        [TestMethod]
        public void EntityNotFound()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();

            var service = new AgreementService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var message = new CreateMappingRequest
            {
                EntityId = 12,
                Mapping = new NexusId { SystemName = "Test", Identifier = "A" }
            };

            validatorFactory.Setup(x => x.IsValid(It.IsAny<CreateMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);

            // Act
            var candidate = service.CreateMapping(message);

            // Assert
            Assert.IsNull(candidate);
        }

        [TestMethod]
        public void ValidContractAdded()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();

            var service = new AgreementService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);
            var identifier = new NexusId { SystemName = "Test", Identifier = "A" };
            var message = new CreateMappingRequest
            {
                EntityId = 12,
                Mapping = identifier
            };

            var system = new MDM.SourceSystem { Name = "Test" };
            var mapping = new AgreementMapping { System = system, MappingValue = "A" };
            validatorFactory.Setup(x => x.IsValid(It.IsAny<CreateMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);
            mappingEngine.Setup(x => x.Map<RWEST.Nexus.MDM.Contracts.NexusId, AgreementMapping>(identifier)).Returns(mapping);

            var agreement = new MDM.Agreement();
            repository.Setup(x => x.FindOne<MDM.Agreement>(12)).Returns(agreement);

            // Act
            var candidate = (AgreementMapping)service.CreateMapping(message);

            // Assert
            Assert.AreSame(mapping, candidate);
            repository.Verify(x => x.Save(agreement));
            repository.Verify(x => x.Flush());
        }
    }
}
    