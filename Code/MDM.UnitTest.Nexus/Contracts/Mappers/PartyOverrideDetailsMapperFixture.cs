namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class PartyOverrideDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            var mockRepository = new Mock<IRepository>();
            var fakeParty = new MDM.Party();
            var fakeBroker = new MDM.Broker();
            var fakeCommodityInstrumentType = new MDM.CommodityInstrumentType();


            mockRepository.Setup(repository => repository.FindOne<MDM.Broker>(1)).Returns(fakeBroker);
            mockRepository.Setup(repository => repository.FindOne<MDM.CommodityInstrumentType>(2)).Returns(fakeCommodityInstrumentType);
            mockRepository.Setup(repository => repository.FindOne<MDM.Party>(3)).Returns(fakeParty);

            // Arrange
            var source = new RWEST.Nexus.MDM.Contracts.PartyOverrideDetails()
            {
                Broker = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
                CommodityInstrumentType = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "2" } },
                MappingValue = "MappingValue",
                Party = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "3" } },
            };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.PartyOverrideDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(fakeBroker, result.Broker);
            Assert.AreEqual(fakeCommodityInstrumentType, result.CommodityInstrumentType);
            Assert.AreEqual(source.MappingValue, result.MappingValue);
            Assert.AreEqual(fakeParty, result.Party);
        }
    }
}