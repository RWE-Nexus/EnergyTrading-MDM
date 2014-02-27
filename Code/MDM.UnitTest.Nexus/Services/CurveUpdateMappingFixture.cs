namespace RWEST.Nexus.MDM.Test.Services
{
    using System;
    using System.Collections.Generic;

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

    using DateRange = RWEST.Nexus.DateRange;
	using SourceSystem = RWEST.Nexus.MDM.SourceSystem;

    [TestClass]
    public class CurveUpdateMappingFixture
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

            var service = new CurveService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // Act
            service.UpdateMapping(null);
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

            var message = new AmendMappingRequest
            {
                EntityId = 12,
                Mapping = new NexusId { SystemName = "Test", Identifier = "A" }
            };

            validatorFactory.Setup(x => x.IsValid(It.IsAny<AmendMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);

            // Act
            var candidate = service.UpdateMapping(message);

            // Assert
            Assert.IsNull(candidate);
        }

        [TestMethod]
        [ExpectedException(typeof(VersionConflictException))]
        public void VersionConflict()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            var service = new CurveService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var message = new AmendMappingRequest
            {
                MappingId = 12,
                Mapping = new NexusId { SystemName = "Test", Identifier = "A" },
                Version = 34
            };

            var mapping = new CurveMapping { Curve = new MDM.Curve() { Timestamp = BitConverter.GetBytes(25L) } };

			// var <%= EntityName.ToLower() %> = new MDM.<%= EntityName %>();
			// <%= EntityName.ToLower() %>.AddDetails(new <%= EntityName %>Details() { Timestamp = BitConverter.GetBytes(25L) });
			// var mapping = new <%= EntityName %>Mapping { <%= EntityName %> =  <%= EntityName.ToLower() %> };

            validatorFactory.Setup(x => x.IsValid(It.IsAny<AmendMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);
            repository.Setup(x => x.FindOne<CurveMapping>(12)).Returns(mapping);

            // Act
            service.UpdateMapping(message);
        }

        [TestMethod]
        public void ValidDetailsSaved()
        {
            const int mappingId = 12;
		
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            var service = new CurveService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // NB Don't put mappingId here - service assigns it
            var identifier = new NexusId { SystemName = "Test", Identifier = "A" };
            var message = new AmendMappingRequest { MappingId = mappingId, Mapping = identifier, Version = 34 };

            var start = new DateTime(2000, 12, 31);
            var finish = DateUtility.Round(SystemTime.UtcNow().AddDays(5));
            var s1 = new SourceSystem { Name = "Test" };
            var m1 = new CurveMapping { Id = mappingId, System = s1, MappingValue = "1", Version = BitConverter.GetBytes(34L), Validity = new DateRange(start, DateUtility.MaxDate) };			
            var m2 = new CurveMapping { Id = mappingId, System = s1, MappingValue = "1", Validity = new DateRange(start, finish) };

            // NB We deliberately bypasses the business logic
            var entity = new MDM.Curve();
            m1.Curve = entity;
            entity.Mappings.Add(m1);

            validatorFactory.Setup(x => x.IsValid(It.IsAny<AmendMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);
            repository.Setup(x => x.FindOne<CurveMapping>(mappingId)).Returns(m1);
            mappingEngine.Setup(x => x.Map<NexusId, CurveMapping>(identifier)).Returns(m2);

            // Act
            service.UpdateMapping(message);

            // Assert
            Assert.AreEqual(mappingId, identifier.MappingId, "Mapping identifier differs");
			// Proves we have an update not an add
			Assert.AreEqual(1, entity.Mappings.Count, "Mapping count differs"); 
            // NB Don't verify result of Update - already covered by CurveMappingFixture
            repository.Verify(x => x.Save(entity));
            repository.Verify(x => x.Flush());
        }
    }
}
	