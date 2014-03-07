namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class VesselDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = new RWEST.Nexus.MDM.Contracts.VesselDetails
                {
					Name = "test"
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.VesselDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
			Assert.AreEqual(source.Name, result.Name);
        }
    }
}
		