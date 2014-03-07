namespace EnergyTrading.MDM.Test.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;
    using EnergyTrading.Search;
    using RWEST.Nexus.MDM;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.MDM.Services;

    [TestClass]
    public class UnitMapFixture
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

            var service = new UnitService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // Act
            service.Map(null);
        }

        [TestMethod]
        public void UnsuccessfulMatchReturnsNotFound()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();
            var service = new UnitService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var list = new List<UnitMapping>();
            repository.Setup(x => x.Queryable<UnitMapping>()).Returns(list.AsQueryable());
			validatorFactory.Setup(x => x.IsValid(It.IsAny<MappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);


            var request = new MappingRequest { SystemName = "Endur", Identifier = "A", ValidAt = SystemTime.UtcNow() };

            // Act
            var contract = service.Map(request);

            // Assert
            repository.Verify(x => x.Queryable<UnitMapping>());
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

            var service = new UnitService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // Domain details
            var start = new DateTime(1999, 12, 31);
            var finish = new DateTime(2020, 1, 1);
            var system = new SourceSystem { Name = "Endur" };
            var mapping = new UnitMapping
            {
                System = system,
                MappingValue = "A"
            };
            var unit = new Unit
            {
                Id = 1,
                Validity = new DateRange(start, finish)
            };
            //Unit.AddDetails(details);
            unit.ProcessMapping(mapping);

            // Contract details
            var identifier = new RWEST.Nexus.MDM.Contracts.NexusId
            {
                SystemName = "Endur",
                Identifier = "A"
            };
            var cDetails = new RWEST.Nexus.MDM.Contracts.UnitDetails();

            mappingEngine.Setup(x => x.Map<UnitMapping, RWEST.Nexus.MDM.Contracts.NexusId>(mapping)).Returns(identifier);
            mappingEngine.Setup(x => x.Map<Unit, RWEST.Nexus.MDM.Contracts.UnitDetails>(unit)).Returns(cDetails);
			validatorFactory.Setup(x => x.IsValid(It.IsAny<MappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);


            var list = new List<UnitMapping> { mapping };
            repository.Setup(x => x.Queryable<UnitMapping>()).Returns(list.AsQueryable());

            var request = new MappingRequest { SystemName = "Endur", Identifier = "A", ValidAt = SystemTime.UtcNow(), Version = 1 };

            // Act
            var response = service.Map(request);
            var candidate = response.Contract;

            // Assert
            mappingEngine.Verify(x => x.Map<UnitMapping, RWEST.Nexus.MDM.Contracts.NexusId>(mapping));
            mappingEngine.Verify(x => x.Map<Unit, RWEST.Nexus.MDM.Contracts.UnitDetails>(unit));
            repository.Verify(x => x.Queryable<UnitMapping>());
            Assert.IsNotNull(candidate, "Contract null");
            Assert.AreEqual(2, candidate.Identifiers.Count, "Identifier count incorrect");
            // NB This is order dependent
            Assert.AreSame(identifier, candidate.Identifiers[1], "Different identifier assigned");
            Assert.AreSame(cDetails, candidate.Details, "Different details assigned");
            Assert.AreEqual(start, candidate.Nexus.StartDate, "Start date differs");
            Assert.AreEqual(finish, candidate.Nexus.EndDate, "End date differs");
        }
    }
}
	