namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
	using EnergyTrading.MDM.Test;

    [TestClass]
    public class PortfolioHierarchyDetailsMapperFixture : Fixture
    {
	    [TestMethod]
        public void Map()
        {
            // Arrange
	        var source = ObjectMother.Create<PortfolioHierarchy>();

            var mapper = new MDM.Mappers.PortfolioHierarchyDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.AreEqual(source.ChildPortfolio.Id.ToString(), result.ChildPortfolio.Identifier.Identifier);
            Assert.AreEqual(source.ParentPortfolio.Id.ToString(), result.ParentPortfolio.Identifier.Identifier);
            Assert.AreEqual(source.Hierarachy.Id.ToString(), result.Hierarchy.Identifier.Identifier);
		}
    }
}

	