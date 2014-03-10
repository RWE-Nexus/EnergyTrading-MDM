namespace EnergyTrading.MDM.Test.Services
{
    using System;
    using System.Collections.Generic;

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
    public class BrokerRateUpdateFixture : Fixture
    {
        [TestMethod,Ignore]
        [ExpectedException(typeof(ValidationException))]
        public void NullContractInvalid()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            validatorFactory.Setup(x => x.IsValid(It.IsAny<object>(), It.IsAny<IList<IRule>>())).Returns(false);
            repository.Setup(x => x.FindOne<BrokerRate>(1)).Returns(new BrokerRate());

            var service = new BrokerRateService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // Act
            service.Update(1, 0, null);
        }

        [TestMethod]
        [ExpectedException(typeof(VersionConflictException))]
        public void EarlierVersionRaisesVersionConflict()
        {
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            validatorFactory.Setup(x => x.IsValid(It.IsAny<RWEST.Nexus.MDM.Contracts.BrokerRate>(), It.IsAny<IList<IRule>>())).Returns(true);

            var service = new BrokerRateService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var cd = new RWEST.Nexus.MDM.Contracts.BrokerRateDetails();
            var nexus = new RWEST.Nexus.MDM.Contracts.SystemData { StartDate = new DateTime(2012, 1, 1) };
            var contract = new RWEST.Nexus.MDM.Contracts.BrokerRate { Details = cd, Nexus = nexus };

            var details = ObjectMother.Create<BrokerRateDetails>();
            details.Id = 1;
            var entity = ObjectMother.Create<BrokerRate>();
            entity.Id = 2;
            entity.AddDetails(details);

            repository.Setup(x => x.FindOne<BrokerRate>(1)).Returns(entity);

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
            var cd = new RWEST.Nexus.MDM.Contracts.BrokerRateDetails();
            var nexus = new RWEST.Nexus.MDM.Contracts.SystemData { StartDate = new DateTime(2012, 1, 1) };
            var identifier = new RWEST.Nexus.MDM.Contracts.NexusId { SystemName = "Test", Identifier = "A" };
            var contract = new RWEST.Nexus.MDM.Contracts.BrokerRate { Details = cd, Nexus = nexus };
            contract.Identifiers.Add(identifier);

            // Domain
            var system = new SourceSystem { Name = "Test" };
            var mapping = new BrokerRateMapping { System = system, MappingValue = "A" };
            var d1 = new BrokerRateDetails();
            d1.Id = 1;
            d1.Timestamp = 74UL.GetVersionByteArray();
            var entity =new BrokerRate();
            //entity.Timestamp = 74UL.GetVersionByteArray();
            entity.AddDetails(d1);

            var d2 = ObjectMother.Create<BrokerRateDetails>(); 
            var range = new DateRange(new DateTime(2012, 1, 1), DateTime.MaxValue);

            validatorFactory.Setup(x => x.IsValid(It.IsAny<CreateMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);
            validatorFactory.Setup(x => x.IsValid(It.IsAny<RWEST.Nexus.MDM.Contracts.BrokerRate>(), It.IsAny<IList<IRule>>())).Returns(true);

            repository.Setup(x => x.FindOne<BrokerRate>(1)).Returns(entity);

            mappingEngine.Setup(x => x.Map<RWEST.Nexus.MDM.Contracts.BrokerRateDetails, BrokerRateDetails>(cd)).Returns(d2);
            mappingEngine.Setup(x => x.Map<RWEST.Nexus.MDM.Contracts.SystemData, DateRange>(nexus)).Returns(range);
            mappingEngine.Setup(x => x.Map<RWEST.Nexus.MDM.Contracts.NexusId, BrokerRateMapping>(identifier)).Returns(mapping);

            var service = new BrokerRateService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // Act
            service.Update(1, 74, contract);


            // Assert
            //Assert.AreEqual(2, entity.Details.Count, "Details count differs");
            //Assert.AreEqual(1, entity.Mappings.Count, "Mapping count differs");
            //repository.Verify(x => x.Save(entity));
            //repository.Verify(x => x.Flush());
            // Assert
            Assert.AreEqual(0, entity.Id, "Created entity id differs");
            Assert.AreEqual(1, entity.Mappings.Count, "Mapping count differs");
            repository.Verify(x => x.Save(entity));
            repository.Verify(x => x.Flush());

            //// Ok, hack the created one to align it
            //d2.Id = entity.Id;
            //foreach (var m in entity.Mappings)
            //{
            //    d2.Mappings.Add(m);
            //}

            //// Should now be the same - avoid exposing what properties we have here
            //Check(d2, entity);
        }

        [TestMethod]
        public void EntityNotFound()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            var service = new BrokerRateService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var cd = new RWEST.Nexus.MDM.Contracts.BrokerRateDetails();
            var nexus = new RWEST.Nexus.MDM.Contracts.SystemData { StartDate = new DateTime(2012, 1, 1) };
            var contract = new RWEST.Nexus.MDM.Contracts.BrokerRate { Details = cd, Nexus = nexus };

            validatorFactory.Setup(x => x.IsValid(It.IsAny<RWEST.Nexus.MDM.Contracts.BrokerRate>(), It.IsAny<IList<IRule>>())).Returns(true);

            // Act
            var response = service.Update(1, 1, contract);

            // Assert
            Assert.IsNotNull(response, "Response is null");
            Assert.IsFalse(response.IsValid, "Response is valid");
            Assert.AreEqual(ErrorType.NotFound, response.Error.Type, "ErrorType differs");
        }
    }
}
	