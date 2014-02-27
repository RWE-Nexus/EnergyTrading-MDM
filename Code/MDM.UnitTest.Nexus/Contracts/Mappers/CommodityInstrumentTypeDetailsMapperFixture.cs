namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class CommodityInstrumentTypeDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            var fakeCommodity = new MDM.Commodity();
            var fakeInstrumentType = new MDM.InstrumentType();

            mockRepository.Setup(repository => repository.FindOne<MDM.Commodity>(1)).Returns(fakeCommodity);
            mockRepository.Setup(repository => repository.FindOne<MDM.InstrumentType>(1)).Returns(fakeInstrumentType);

            var source = new RWEST.Nexus.MDM.Contracts.CommodityInstrumentTypeDetails
                {
                    Commodity = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
                    InstrumentType = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
                    InstrumentDelivery = "physical",
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.CommodityInstrumentTypeDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(fakeCommodity, result.Commodity);
            Assert.AreEqual(fakeInstrumentType, result.InstrumentType);
            Assert.AreEqual(source.InstrumentDelivery, result.InstrumentDelivery);
        }
    }
}