namespace RWEST.Nexus.MDM.Test.Services
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using RWEST.Nexus;
    using RWEST.Nexus.Data;
    using RWEST.Nexus.Mapping;
    using RWEST.Nexus.Validation;
    using RWEST.Nexus.Search;
    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Messages;
    using RWEST.Nexus.MDM.Services;

    [TestClass]
    public class CurveUpdateFixture : Fixture
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

            validatorFactory.Setup(x => x.IsValid(It.IsAny<object>(), It.IsAny<IList<IRule>>())).Returns(false);
            repository.Setup(x => x.FindOne<Curve>(1)).Returns(new Curve());

            var service = new CurveService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

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

            validatorFactory.Setup(x => x.IsValid(It.IsAny<MDM.Contracts.Curve>(), It.IsAny<IList<IRule>>())).Returns(true);

            var service = new CurveService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var cd = new MDM.Contracts.CurveDetails();
            var nexus = new MDM.Contracts.SystemData { StartDate = new DateTime(2012, 1, 1) };
            var contract = new MDM.Contracts.Curve { Details = cd, Nexus = nexus };

            var details = ObjectMother.Create<Curve>();
            details.Id = 1;
            var entity = ObjectMother.Create<Curve>();
            entity.Id = 2;
            entity.AddDetails(details);

            repository.Setup(x => x.FindOne<Curve>(1)).Returns(entity);

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
            var cd = new MDM.Contracts.CurveDetails();
            var nexus = new MDM.Contracts.SystemData { StartDate = new DateTime(2012, 1, 1) };
            var identifier = new MDM.Contracts.NexusId { SystemName = "Test", Identifier = "A" };
            var contract = new MDM.Contracts.Curve { Details = cd, Nexus = nexus };
            contract.Identifiers.Add(identifier);

            // Domain
            var system = new SourceSystem { Name = "Test" };
            var mapping = new CurveMapping { System = system, MappingValue = "A" };
            var d1 = ObjectMother.Create<Curve>();
            d1.Id = 1;
            d1.Timestamp = new byte[] { 74, 0, 0, 0, 0, 0, 0, 0 };
            var entity = ObjectMother.Create<Curve>(); 
            entity.Timestamp = new byte[] { 74, 0, 0, 0, 0, 0, 0, 0 };
            entity.AddDetails(d1);

            var d2 = ObjectMother.Create<Curve>(); 
            var range = new DateRange(new DateTime(2012, 1, 1), DateTime.MaxValue);

            validatorFactory.Setup(x => x.IsValid(It.IsAny<CreateMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);
            validatorFactory.Setup(x => x.IsValid(It.IsAny<MDM.Contracts.Curve>(), It.IsAny<IList<IRule>>())).Returns(true);

            repository.Setup(x => x.FindOne<Curve>(1)).Returns(entity);

            mappingEngine.Setup(x => x.Map<MDM.Contracts.CurveDetails, Curve>(cd)).Returns(d2);
            mappingEngine.Setup(x => x.Map<MDM.Contracts.SystemData, DateRange>(nexus)).Returns(range);
            mappingEngine.Setup(x => x.Map<MDM.Contracts.NexusId, CurveMapping>(identifier)).Returns(mapping);

            var service = new CurveService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // Act
            service.Update(1, 74, contract);

            // Assert
            Assert.AreEqual(0, d2.Mappings.Count, "Created entity mappings count differs");
            Assert.AreEqual(0, d2.Id, "Created entity id differs");

            Assert.AreEqual(1, entity.Mappings.Count, "Mapping count differs");
            repository.Verify(x => x.Save(entity));
            repository.Verify(x => x.Flush());

            // Ok, hack the created one to align it
            d2.Id = entity.Id;
            foreach (var m in entity.Mappings)
            {
                d2.Mappings.Add(m);
            }

            // Should now be the same - avoid exposing what properties we have here
            Check(d2, entity);
        }

        [TestMethod]
        public void EntityNotFound()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            var service = new CurveService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var cd = new MDM.Contracts.CurveDetails();
            var nexus = new MDM.Contracts.SystemData { StartDate = new DateTime(2012, 1, 1) };
            var contract = new MDM.Contracts.Curve { Details = cd, Nexus = nexus };

            validatorFactory.Setup(x => x.IsValid(It.IsAny<MDM.Contracts.Curve>(), It.IsAny<IList<IRule>>())).Returns(true);

            // Act
            var response = service.Update(1, 1, contract);

            // Assert
            Assert.IsNotNull(response, "Response is null");
            Assert.IsFalse(response.IsValid, "Response is valid");
            Assert.AreEqual(ErrorType.NotFound, response.Error.Type, "ErrorType differs");
        }
    }
}
	