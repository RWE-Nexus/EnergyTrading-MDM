namespace EnergyTrading.MDM.Test.Mappers
{
    using NUnit.Framework;

    [TestFixture]
    public class SourceSystemDetailsMapperFixture : Fixture
    {
        [Test]
        public void Map()
        {
            // Arrange
            var source = new MDM.SourceSystem()
                {
                };

            var mapper = new MDM.Mappers.SourceSystemDetailsMapper();

            // Act
            var result = mapper.Map(source);

			// Assert
			Assert.IsNotNull(result);
        }
    }
}

	