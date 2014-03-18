namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Data;

    [TestClass]
    public class SourceSystemDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();

            var source = new EnergyTrading.Mdm.Contracts.SourceSystemDetails
                {
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.SourceSystemDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);

        }
    }
}
		