namespace EnergyTrading.Mdm.Test.Mappers
{
    using NUnit.Framework;

    [TestFixture]
    public class SourceSystemDetailsMapperFixture : Fixture
    {
        [Test]
        public void Map()
        {
            // Arrange
            var source = new Mdm.SourceSystem
            {
            };

            var mapper = new Mdm.Mappers.SourceSystemDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}