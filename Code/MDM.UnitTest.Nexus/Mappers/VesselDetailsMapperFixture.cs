namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
	using EnergyTrading.MDM.Test;

    [TestClass]
    public class VesselDetailsMapperFixture : Fixture
    {
	    [TestMethod]
        public void Map()
        {
            // Arrange
	        var source = ObjectMother.Create<Vessel>();

            var mapper = new MDM.Mappers.VesselDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
			
            Assert.AreEqual(source.Name, result.Name);
            
		}
    }
}

	