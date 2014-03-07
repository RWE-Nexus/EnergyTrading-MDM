namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
	using EnergyTrading.MDM.Test;

    [TestClass]
    public class DimensionDetailsMapperFixture : Fixture
    {
	    [TestMethod]
        public void Map()
        {
            // Arrange
	        var source = ObjectMother.Create<Dimension>();

            var mapper = new MDM.Mappers.DimensionDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
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