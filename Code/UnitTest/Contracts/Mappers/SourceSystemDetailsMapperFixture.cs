namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using NUnit.Framework;

    using Moq;

    using EnergyTrading.Data;

    [TestFixture]
    public class SourceSystemDetailsMapperFixture : Fixture
    {
        [Test]
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
		