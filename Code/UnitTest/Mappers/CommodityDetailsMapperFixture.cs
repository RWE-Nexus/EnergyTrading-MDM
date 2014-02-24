namespace EnergyTrading.MDM.Test.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CommodityDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = new MDM.Commodity()
                {
                    Name = "Test Commodity",
                    Parent = new Commodity() { Name = "ParentCommodity" },
                    MassEnergyUnits = new Unit(){Name = "KcalPerKilogram", Symbol = "Kcal/kg"},
                    MassEnergyValue = 1400,
                    VolumeEnergyUnits = new Unit(){Name = "MegaJoulPerCubicMeter", Symbol = "MJ/m^3"},
                    VolumeEnergyValue = 49.5m,
                    VolumetricDensityUnits = new Unit(){Name = "API Gravity", Symbol = "ApiG"},
                    VolumetricDensityValue = 234.33m,
                    DeliveryRate = "Hour"
                };

            var mapper = new MDM.Mappers.CommodityDetailsMapper();

            // Act
            var result = mapper.Map(source);

			// Assert
			Assert.IsNotNull(result);
            Assert.AreEqual("Test Commodity", result.Name);
            Assert.IsNotNull(result.Parent);
            Assert.AreEqual("ParentCommodity", result.Parent.Name);
            Assert.IsNotNull(result.MassEnergyUnits);
            Assert.AreEqual("KcalPerKilogram", result.MassEnergyUnits.Name);
            Assert.IsNotNull(result.VolumeEnergyUnits);
            Assert.AreEqual("MegaJoulPerCubicMeter", result.VolumeEnergyUnits.Name);
            Assert.IsNotNull(result.VolumetricDensityUnits);
            Assert.AreEqual("API Gravity", result.VolumetricDensityUnits.Name);
            Assert.AreEqual("Hour", result.DeliveryRate);
            Assert.AreEqual(1400, result.MassEnergyValue);
            Assert.AreEqual(49.5m, result.VolumeEnergyValue);
            Assert.AreEqual(234.33m, result.VolumetricDensityValue);
        }
    }
}

	