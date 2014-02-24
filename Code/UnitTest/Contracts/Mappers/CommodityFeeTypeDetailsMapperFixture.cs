namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;
    

    [TestClass]
    public class CommodityFeeTypeDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            var fakeCommodity = new MDM.Commodity();
            var fakeFeeType = new MDM.FeeType();

            mockRepository.Setup(repository => repository.FindOne<MDM.Commodity>(1)).Returns(fakeCommodity);
            mockRepository.Setup(repository => repository.FindOne<MDM.FeeType>(1)).Returns(fakeFeeType);
            var source = new RWEST.Nexus.MDM.Contracts.CommodityFeeTypeDetails
                {
                    Commodity = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
                    FeeType = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.CommodityFeeTypeDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(fakeCommodity, result.Commodity);
            Assert.AreEqual(fakeFeeType, result.FeeType);
          }
    }
}