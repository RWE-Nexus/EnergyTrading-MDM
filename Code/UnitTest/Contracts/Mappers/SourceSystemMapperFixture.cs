namespace EnergyTrading.Mdm.Test.Contracts.Mappers
{
    using System;

    using EnergyTrading;
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts.Mappers;
    using EnergyTrading.Mdm.ServiceHost.Unity.Configuration;

    using Microsoft.Practices.Unity;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class SourceSystemMapperFixture : Fixture
    {
        [Test]
        public void Resolve()
        {
            var container = CreateContainer();
            var meConfig = new SimpleMappingEngineConfiguration(container);
            meConfig.Configure();

            var config = new SourceSystemConfiguration(container);
            config.Configure();

            var validator = container.Resolve<IMapper<EnergyTrading.Mdm.Contracts.SourceSystem, SourceSystem>>();

            // Assert
            Assert.IsNotNull(validator, "Mapper resolution failed");
        }

        [Test]
        public void Map()
        {
            // Arrange
            var start = new DateTime(2010, 1, 1);
            var end = DateUtility.MaxDate;
            var range = new DateRange(start, end);

            var id = new EnergyTrading.Mdm.Contracts.MdmId { SystemName = "Test", Identifier = "A" };
            var contractDetails = new EnergyTrading.Mdm.Contracts.SourceSystemDetails();
            var contract = new EnergyTrading.Mdm.Contracts.SourceSystem
            {
                Identifiers = new EnergyTrading.Mdm.Contracts.MdmIdList { id },
                Details = contractDetails,
                MdmSystemData = new EnergyTrading.Mdm.Contracts.SystemData { StartDate = start, EndDate = end }
            };

            // NB Don't assign validity here, want to prove SUT sets it
            var details = new SourceSystem();

            var mapping = new SourceSystemMapping();

            var mappingEngine = new Mock<IMappingEngine>();
            mappingEngine.Setup(x => x.Map<EnergyTrading.Mdm.Contracts.MdmId, SourceSystemMapping>(id)).Returns(mapping);
            mappingEngine.Setup(x => x.Map<EnergyTrading.Mdm.Contracts.SourceSystemDetails, SourceSystem>(contractDetails)).Returns(details);

            var mapper = new SourceSystemMapper(mappingEngine.Object);

            // Act
            var candidate = mapper.Map(contract);

            // Assert
            //Assert.AreEqual(1, candidate.Details.Count, "Detail count differs");
            Assert.AreEqual(1, candidate.Mappings.Count, "Mapping count differs");
            Check(range, details.Validity, "Validity differs");
        }
    }
}
