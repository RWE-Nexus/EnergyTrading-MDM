﻿namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using System;

    using EnergyTrading.MDM.ServiceHost.Unity.Configuration;

    using global::MDM.ServiceHost.Unity.Nexus.Configuration;

    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading;
    using EnergyTrading.Mapping;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class TenorTypeMapperFixture : Fixture
    {
        [TestMethod]
        public void Resolve()
        {
            var container = CreateContainer();
            var meConfig = new SimpleMappingEngineConfiguration(container);
            meConfig.Configure();

            var config = new TenorTypeConfiguration(container);
            config.Configure();

            var validator = container.Resolve<IMapper<RWEST.Nexus.MDM.Contracts.TenorType, TenorType>>();

            // Assert
            Assert.IsNotNull(validator, "Mapper resolution failed");
        }

        [TestMethod]
        public void Map()
        {
            // Arrange
            var start = new DateTime(2010, 1, 1);
            var end = DateUtility.MaxDate;
            var range = new DateRange(start, end);

            var id = new RWEST.Nexus.MDM.Contracts.NexusId { SystemName = "Test", Identifier = "A" };
            var contractDetails = new RWEST.Nexus.MDM.Contracts.TenorTypeDetails();
            var contract = new RWEST.Nexus.MDM.Contracts.TenorType
            {
                Identifiers = new RWEST.Nexus.MDM.Contracts.NexusIdList { id },
                Details = contractDetails,
                Nexus = new RWEST.Nexus.MDM.Contracts.SystemData { StartDate = start, EndDate = end }
            };

            // NB Don't assign validity here, want to prove SUT sets it
            var details = new TenorType();

            var mapping = new TenorTypeMapping();

            var mappingEngine = new Mock<IMappingEngine>();
            mappingEngine.Setup(x => x.Map<RWEST.Nexus.MDM.Contracts.NexusId, TenorTypeMapping>(id)).Returns(mapping);
            mappingEngine.Setup(x => x.Map<RWEST.Nexus.MDM.Contracts.TenorTypeDetails, TenorType>(contractDetails)).Returns(details);

            var mapper = new TenorTypeMapper(mappingEngine.Object);

            // Act
            var candidate = mapper.Map(contract);

            // Assert
            //Assert.AreEqual(1, candidate.Details.Count, "Detail count differs");
            Assert.AreEqual(1, candidate.Mappings.Count, "Mapping count differs");
            Check(range, details.Validity, "Validity differs");
        }
    }
}
