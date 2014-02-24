using Moq;
using EnergyTrading.Data;
using RWEST.Nexus.MDM.Contracts;

namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProductCurveDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            var mockRepository = new Mock<IRepository>();
            var fakeCurve = new MDM.Curve();
            var fakeProduct = new MDM.Product();

            
            mockRepository.Setup(repository => repository.FindOne<MDM.Product>(1)).Returns(fakeProduct);
            mockRepository.Setup(repository => repository.FindOne<MDM.Curve>(4)).Returns(fakeCurve);

            // Arrange
            var source = new RWEST.Nexus.MDM.Contracts.ProductCurveDetails
                {
                    Name = "testName",
                    Product = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
					Curve = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "4" } },
                    ProductCurveType = "Financial",
                    ProjectionMethod = "Monthly"
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.ProductCurveDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
			Assert.AreEqual(source.Name, result.Name);
			Assert.AreEqual(fakeProduct, result.Product);
			Assert.AreEqual(fakeCurve, result.Curve);
			Assert.AreEqual(source.ProductCurveType, result.ProductCurveType);
			Assert.AreEqual(source.Name, result.Name);
        }
    }
}
		