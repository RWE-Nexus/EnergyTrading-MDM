namespace EnergyTrading.MDM.Test.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ShipperCodeDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = new MDM.ShipperCode()
                {
                };

            var mapper = new MDM.Mappers.ShipperCodeDetailsMapper();

            // Act
            var result = mapper.Map(source);

			// Assert
			Assert.IsNotNull(result);
        }
    }
}

	