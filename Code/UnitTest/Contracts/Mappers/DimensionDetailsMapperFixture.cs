namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DimensionDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = new RWEST.Nexus.MDM.Contracts.DimensionDetails
                {
                    Name = "testDimension",
                    Description = "Blah Blah Dimension",
                    ElectricCurrentDimension = 1,
                    LengthDimension = 3,
                    LuminousIntensityDimension = 5,
                    TemperatureDimension = 8,
                    MassDimension = 2,
                    TimeDimension = 7
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.DimensionDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.Description, result.Description);
            Assert.AreEqual(source.ElectricCurrentDimension, result.ElectricCurrentDimension);
            Assert.AreEqual(source.LengthDimension, result.LengthDimension);
            Assert.AreEqual(source.LuminousIntensityDimension, result.LuminousIntensityDimension);
            Assert.AreEqual(source.MassDimension, result.MassDimension);
            Assert.AreEqual(source.TimeDimension, result.TimeDimension);
            Assert.AreEqual(source.TemperatureDimension, result.TemperatureDimension);
        }
    }
}		