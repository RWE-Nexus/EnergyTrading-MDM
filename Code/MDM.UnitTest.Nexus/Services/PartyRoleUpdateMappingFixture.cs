namespace EnergyTrading.MDM.Test.Services
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    using RWEST.Nexus.MDM;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.MDM.Services;

    using DateRange = EnergyTrading.DateRange;
    using PartyRoleDetails = EnergyTrading.MDM.PartyRoleDetails;

    [TestClass]
    public class PartyRoleUpdateMappingFixture
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

            var service = new PartyRoleService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

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

            var service = new PartyRoleService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

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

            var service = new PartyRoleService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var message = new AmendMappingRequest
            {
                MappingId = 12,
                Mapping = new NexusId { SystemName = "Test", Identifier = "A" },
                Version = 34
            };

            var party = new MDM.PartyRole();
            party.AddDetails(new PartyRoleDetails() { Timestamp = BitConverter.GetBytes(25L) });
            var mapping = new PartyRoleMapping { PartyRole =  party };

            validatorFactory.Setup(x => x.IsValid(It.IsAny<AmendMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);
            repository.Setup(x => x.FindOne<PartyRoleMapping>(12)).Returns(mapping);

            // Act
            service.UpdateMapping(message);
        }

        [TestMethod]
        public void ValidDetailsSaved()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();

            var service = new PartyRoleService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var identifier = new NexusId { SystemName = "Test", Identifier = "A" };
            var message = new AmendMappingRequest { MappingId = 12, Mapping = identifier, Version = 34 };

            var start = new DateTime(2000, 12, 31);
            var finish = DateUtility.Round(SystemTime.UtcNow().AddDays(5));
            var s1 = new MDM.SourceSystem { Name = "Test" };
            var m1 = new PartyRoleMapping { Id = 12, System = s1, MappingValue = "1", Version = 34UL.GetVersionByteArray(), Validity = new DateRange(start, DateUtility.MaxDate) };
            var m2 = new PartyRoleMapping { Id = 12, System = s1, MappingValue = "1", Validity = new DateRange(start, finish) };

            // NB We deliberately bypasses the business logic
            var party = new MDM.PartyRole();
            m1.PartyRole = party;
            party.Mappings.Add(m1);

            validatorFactory.Setup(x => x.IsValid(It.IsAny<AmendMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);
            repository.Setup(x => x.FindOne<PartyRoleMapping>(12)).Returns(m1);
            mappingEngine.Setup(x => x.Map<RWEST.Nexus.MDM.Contracts.NexusId, PartyRoleMapping>(identifier)).Returns(m2);

            // Act
            service.UpdateMapping(message);

            // Assert
            // NB Don't verify result of Update - already covered by PartyRoleMappingFixture
            repository.Verify(x => x.Save(party));
            repository.Verify(x => x.Flush());
        }
    }
}

