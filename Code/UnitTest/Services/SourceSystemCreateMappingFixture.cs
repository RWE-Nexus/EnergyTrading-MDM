namespace EnergyTrading.Mdm.Test.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Messages;
    using EnergyTrading.Mdm.Services;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class SourceSystemCreateMappingFixture : Fixture
    {
        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void NullContractInvalid()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();

            var service = new SourceSystemService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            validatorFactory.Setup(x => x.IsValid(It.IsAny<object>(), It.IsAny<IList<IRule>>())).Returns(false);

            // Act
            service.CreateMapping(null);
        }

        [Test]
        public void EntityNotFound()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();

            var service = new SourceSystemService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var message = new CreateMappingRequest
            {
                EntityId = 12,
                Mapping = new EnergyTrading.Mdm.Contracts.MdmId { SystemName = "Test", Identifier = "A" }
            };

            validatorFactory.Setup(x => x.IsValid(It.IsAny<CreateMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);

            // Act
            var candidate = service.CreateMapping(message);

            // Assert
            Assert.IsNull(candidate);
        }

        [Test]
        public void ValidContractAdded()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();

            var service = new SourceSystemService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);
            var identifier = new EnergyTrading.Mdm.Contracts.MdmId { SystemName = "Test", Identifier = "A" };
            var message = new CreateMappingRequest
            {
                EntityId = 12,
                Mapping = identifier
            };

            var system = new Mdm.SourceSystem { Name = "Test" };
            var mapping = new SourceSystemMapping { System = system, MappingValue = "A" };
            validatorFactory.Setup(x => x.IsValid(It.IsAny<CreateMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);
            mappingEngine.Setup(x => x.Map<EnergyTrading.Mdm.Contracts.MdmId, SourceSystemMapping>(identifier)).Returns(mapping);

            var sourcesystem = new Mdm.SourceSystem();
            repository.Setup(x => x.FindOne<Mdm.SourceSystem>(12)).Returns(sourcesystem);

            // Act
            var candidate = (SourceSystemMapping)service.CreateMapping(message);

            // Assert
            Assert.AreSame(mapping, candidate);
            repository.Verify(x => x.Save(sourcesystem));
            repository.Verify(x => x.Flush());
        }
    }
}