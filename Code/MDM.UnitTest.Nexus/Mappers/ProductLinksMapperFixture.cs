namespace EnergyTrading.MDM.Test.Mappers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RWEST.Nexus.Contracts.Atom;

    [TestClass]
    public class ProductLinksMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var productCurves = new List<ProductCurve>
                                    {
                                        new ProductCurve {Id = 1},
                                        new ProductCurve {Id = 2}
                                    };

            var source = new MDM.Product() 
            { 
                ProductCurves = productCurves
                };

            var mapper = new MDM.Mappers.ProductLinksMapper();

            // Act
            var results = mapper.Map(source);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);

            AssertLink(1, results);
            AssertLink(2, results);
        }

        private static void AssertLink(int id, IEnumerable<Link> results)
        {
            var link = results.FirstOrDefault(x => x.Uri == string.Format("/ProductCurve/{0}", id));
            Assert.IsNotNull(link);
            Assert.AreEqual("get-related-productcurve", link.Rel);
            Assert.AreEqual("ProductCurve", link.Type);
        }
    }
}


