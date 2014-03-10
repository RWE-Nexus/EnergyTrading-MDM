namespace EnergyTrading.MDM.Test.Services
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using RWEST.Nexus.MDM;
    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.MDM.Services;

    [TestClass]
    public class CounterpartyUpdateFixture
    {
        [TestMethod]
        [ExpectedException(typeof(VersionConflictException))]
        public void EarlierVersionRaisesVersionConflict()
        {
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();

            validatorFactory.Setup(x => x.IsValid(It.IsAny<RWEST.Nexus.MDM.Contracts.Counterparty>(), It.IsAny<IList<IRule>>())).Returns(true);

            var service = new CounterpartyService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var cd = new RWEST.Nexus.MDM.Contracts.CounterpartyDetails { Name = "Test" };
            var nexus = new RWEST.Nexus.MDM.Contracts.SystemData { StartDate = new DateTime(2012, 1, 1) };
            var contract = new RWEST.Nexus.MDM.Contracts.Counterparty { Details = cd, Nexus = nexus };

            var details = new CounterpartyDetails { Id = 2, Name = "Test" };
            var entity = new Counterparty();
            entity.AddDetails(details);

            repository.Setup(x => x.FindOne<Counterparty>(1)).Returns(entity);

            // Act
            service.Update(1, 1, contract);
        }

        [TestMethod]
        public void ValidDetailsSaved()
        {
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();

            // Contract
            var cd = new RWEST.Nexus.MDM.Contracts.CounterpartyDetails { Name = "Test" };
            var nexus = new RWEST.Nexus.MDM.Contracts.SystemData { StartDate = new DateTime(2012, 1, 1) };
            var identifier = new RWEST.Nexus.MDM.Contracts.NexusId { SystemName = "Test", Identifier = "A" };
            var contract = new RWEST.Nexus.MDM.Contracts.Counterparty { Details = cd, Nexus = nexus };
            contract.Identifiers.Add(identifier);

            // Domain
            var system = new SourceSystem { Name = "Test" };
            var mapping = new PartyRoleMapping { System = system, MappingValue = "A" };
            var d1 = new CounterpartyDetails { Id = 1, Name = "Test", Timestamp = 74UL.GetVersionByteArray() };
            var entity = new Counterparty();
            entity.Party = new Party() { Id = 1};
            entity.AddDetails(d1);

            var d2 = new CounterpartyDetails { Name = "Test" };
            var range = new DateRange(new DateTime(2012, 1, 1), DateTime.MaxValue);

            validatorFactory.Setup(x => x.IsValid(It.IsAny<CreateMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);
            validatorFactory.Setup(x => x.IsValid(It.IsAny<RWEST.Nexus.MDM.Contracts.Counterparty>(), It.IsAny<IList<IRule>>())).Returns(true);

            repository.Setup(x => x.FindOne<Counterparty>(1)).Returns(entity);

            mappingEngine.Setup(x => x.Map<RWEST.Nexus.MDM.Contracts.CounterpartyDetails, CounterpartyDetails>(cd)).Returns(d2);
            mappingEngine.Setup(x => x.Map<RWEST.Nexus.MDM.Contracts.SystemData, DateRange>(nexus)).Returns(range);
            mappingEngine.Setup(x => x.Map<RWEST.Nexus.MDM.Contracts.NexusId, PartyRoleMapping>(identifier)).Returns(mapping);

            var service = new CounterpartyService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // Act
            service.Update(1, 74, contract);

            // Assert
            Assert.AreEqual(2, entity.Details.Count, "Details count differs");
            Assert.AreEqual(1, entity.Mappings.Count, "Mapping count differs");
            repository.Verify(x => x.Save(entity));
            repository.Verify(x => x.Flush());
        }

        [TestMethod]
        public void EntityNotFound()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();

            var service = new CounterpartyService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var cd = new RWEST.Nexus.MDM.Contracts.CounterpartyDetails { Name = "Test" };
            var nexus = new RWEST.Nexus.MDM.Contracts.SystemData { StartDate = new DateTime(2012, 1, 1) };
            var contract = new RWEST.Nexus.MDM.Contracts.Counterparty { Details = cd, Nexus = nexus };

            validatorFactory.Setup(x => x.IsValid(It.IsAny<RWEST.Nexus.MDM.Contracts.Counterparty>(), It.IsAny<IList<IRule>>())).Returns(true);

            // Act
            var response = service.Update(1, 1, contract);

            // Assert
            Assert.IsNotNull(response, "Response is null");
            Assert.IsFalse(response.IsValid, "Response is valid");
            Assert.AreEqual(ErrorType.NotFound, response.Error.Type, "ErrorType differs");
        }
    }
}
	