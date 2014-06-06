namespace EnergyTrading.Mdm.Test.Services
{
    using System;
    using System.Collections.Generic;

    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm;
    using EnergyTrading.Mdm.Messages;
    using EnergyTrading.Mdm.Services;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class SourceSystemUpdateFixture : Fixture
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

            validatorFactory.Setup(x => x.IsValid(It.IsAny<object>(), It.IsAny<IList<IRule>>())).Returns(false);
            repository.Setup(x => x.FindOne<SourceSystem>(1)).Returns(new SourceSystem());

            var service = new SourceSystemService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // Act
            service.Update(1, 0, null);
        }

        [Test]
        [ExpectedException(typeof(VersionConflictException))]
        public void EarlierVersionRaisesVersionConflict()
        {
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();

            validatorFactory.Setup(x => x.IsValid(It.IsAny<EnergyTrading.Mdm.Contracts.SourceSystem>(), It.IsAny<IList<IRule>>())).Returns(true);

            var service = new SourceSystemService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var cd = new EnergyTrading.Mdm.Contracts.SourceSystemDetails();
            var nexus = new EnergyTrading.Mdm.Contracts.SystemData { StartDate = new DateTime(2012, 1, 1) };
            var contract = new EnergyTrading.Mdm.Contracts.SourceSystem { Details = cd, MdmSystemData = nexus };

            var details = ObjectMother.Create<SourceSystem>();
            details.Id = 1;
            var entity = ObjectMother.Create<SourceSystem>();
            entity.Id = 2;
            entity.AddDetails(details);

            repository.Setup(x => x.FindOne<SourceSystem>(1)).Returns(entity);

            // Act
            service.Update(1, 1, contract);
        }

        [Test]
        public void ValidDetailsSaved()
        {
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();

            // Contract
            var cd = new EnergyTrading.Mdm.Contracts.SourceSystemDetails();
            var nexus = new EnergyTrading.Mdm.Contracts.SystemData { StartDate = new DateTime(2012, 1, 1) };
            var identifier = new EnergyTrading.Mdm.Contracts.MdmId { SystemName = "Test", Identifier = "A" };
            var contract = new EnergyTrading.Mdm.Contracts.SourceSystem { Details = cd, MdmSystemData = nexus };
            contract.Identifiers.Add(identifier);

            // Domain
            var system = new SourceSystem { Name = "Test" };
            var mapping = new SourceSystemMapping { System = system, MappingValue = "A" };
            var d1 = ObjectMother.Create<SourceSystem>();
            d1.Id = 1;
            d1.Timestamp = 74UL.GetVersionByteArray();
            var entity = ObjectMother.Create<SourceSystem>();
            entity.Timestamp = 74UL.GetVersionByteArray();
            entity.AddDetails(d1);

            var d2 = ObjectMother.Create<SourceSystem>(); 
            var range = new DateRange(new DateTime(2012, 1, 1), DateTime.MaxValue);

            validatorFactory.Setup(x => x.IsValid(It.IsAny<CreateMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);
            validatorFactory.Setup(x => x.IsValid(It.IsAny<EnergyTrading.Mdm.Contracts.SourceSystem>(), It.IsAny<IList<IRule>>())).Returns(true);

            repository.Setup(x => x.FindOne<SourceSystem>(1)).Returns(entity);

            mappingEngine.Setup(x => x.Map<EnergyTrading.Mdm.Contracts.SourceSystemDetails, SourceSystem>(cd)).Returns(d2);
            mappingEngine.Setup(x => x.Map<EnergyTrading.Mdm.Contracts.SystemData, DateRange>(nexus)).Returns(range);
            mappingEngine.Setup(x => x.Map<EnergyTrading.Mdm.Contracts.MdmId, SourceSystemMapping>(identifier)).Returns(mapping);

            var service = new SourceSystemService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

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

        [Test]
        public void EntityNotFound()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();

            var service = new SourceSystemService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var cd = new EnergyTrading.Mdm.Contracts.SourceSystemDetails();
            var nexus = new EnergyTrading.Mdm.Contracts.SystemData { StartDate = new DateTime(2012, 1, 1) };
            var contract = new EnergyTrading.Mdm.Contracts.SourceSystem { Details = cd, MdmSystemData = nexus };

            validatorFactory.Setup(x => x.IsValid(It.IsAny<EnergyTrading.Mdm.Contracts.SourceSystem>(), It.IsAny<IList<IRule>>())).Returns(true);

            // Act
            var response = service.Update(1, 1, contract);

            // Assert
            Assert.IsNotNull(response, "Response is null");
            Assert.IsFalse(response.IsValid, "Response is valid");
            Assert.AreEqual(ErrorType.NotFound, response.Error.Type, "ErrorType differs");
        }
    }
}