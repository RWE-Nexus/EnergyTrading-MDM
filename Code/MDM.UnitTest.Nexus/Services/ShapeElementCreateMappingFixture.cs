namespace EnergyTrading.MDM.Test.Services
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;
    using EnergyTrading.Search;
    using RWEST.Nexus.MDM;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.MDM.Services;

    [TestClass]
    public class ShapeElementCreateMappingFixture : Fixture
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

            var service = new ShapeElementService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

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

            var service = new ShapeElementService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

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

            var service = new ShapeElementService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);
            var identifier = new NexusId { SystemName = "Test", Identifier = "A" };
            var message = new CreateMappingRequest
            {
                EntityId = 12,
                Mapping = identifier
            };

            var system = new MDM.SourceSystem { Name = "Test" };
            var mapping = new ShapeElementMapping { System = system, MappingValue = "A" };
            validatorFactory.Setup(x => x.IsValid(It.IsAny<CreateMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);
            mappingEngine.Setup(x => x.Map<RWEST.Nexus.MDM.Contracts.NexusId, ShapeElementMapping>(identifier)).Returns(mapping);

            var shapeelement = new MDM.ShapeElement();
            repository.Setup(x => x.FindOne<MDM.ShapeElement>(12)).Returns(shapeelement);

            // Act
            var candidate = (ShapeElementMapping)service.CreateMapping(message);

            // Assert
            Assert.AreSame(mapping, candidate);
            repository.Verify(x => x.Save(shapeelement));
            repository.Verify(x => x.Flush());
        }
    }
}
	