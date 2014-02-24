namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Data;

    [TestClass]
    public class LocationDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();

            var source = new RWEST.Nexus.MDM.Contracts.LocationDetails
                {
                   Name = "Test Name",
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.LocationDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, source.Name);
        }
    }
}
		