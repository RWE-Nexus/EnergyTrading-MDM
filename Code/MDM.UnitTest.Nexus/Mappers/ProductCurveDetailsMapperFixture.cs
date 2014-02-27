namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
	using EnergyTrading.MDM.Test;

    [TestClass]
    public class ProductCurveDetailsMapperFixture : Fixture
    {
	    [TestMethod]
        public void Map()
        {
            // Arrange
	        var source = ObjectMother.Create<ProductCurve>();

            var mapper = new MDM.Mappers.ProductCurveDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
			
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.Product.Id.ToString(), result.Product.Identifier.Identifier);
            Assert.AreEqual(source.Curve.Id.ToString(), result.Curve.Identifier.Identifier);
            Assert.AreEqual(source.ProductCurveType, result.ProductCurveType);
            Assert.AreEqual(source.ProjectionMethod, result.ProjectionMethod);
            
		}
    }
}

	