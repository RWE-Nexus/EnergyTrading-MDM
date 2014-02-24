namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Data;

    [TestClass]
    public class ShipperCodeDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();

            var source = new RWEST.Nexus.MDM.Contracts.ShipperCodeDetails
                {
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.ShipperCodeDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);

        }
    }
}
		