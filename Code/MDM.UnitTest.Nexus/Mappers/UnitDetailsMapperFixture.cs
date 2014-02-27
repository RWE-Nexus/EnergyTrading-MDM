namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
	using EnergyTrading.MDM.Test;

    [TestClass]
    public class UnitDetailsMapperFixture : Fixture
    {
	    [TestMethod]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<Unit>();
            
            var mapper = new MDM.Mappers.UnitDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.Description, result.Description);
            Assert.AreEqual(source.ConversionConstant, result.ConversionConstant);
            Assert.AreEqual(source.ConversionFactor, result.ConversionFactor);
            Assert.AreEqual(source.Symbol, result.Symbol);
            Assert.AreEqual(source.Dimension.Id.ToString(), result.Dimension.Identifier.Identifier.ToString());
		}
    }
}