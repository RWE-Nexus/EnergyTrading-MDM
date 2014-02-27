namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using System.Globalization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;
    using TenorType = EnergyTrading.MDM.TenorType;
    using Product = EnergyTrading.MDM.Product;

    [TestClass]
    public class ProductTenorTypeDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            var product = new Product
            {
                Id = 1,
                Name = "product",
            };
            var tenorType = new TenorType
            {
                Id = 2,
                Name = "tenorType",
            };
            var source = new ProductTenorTypeDetails
            {
                Product = CreateEntityId(product),
                TenorType = CreateEntityId(tenorType),
            };
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.FindOne<Product>(product.Id)).Returns(product);
            mockRepository.Setup(x => x.FindOne<TenorType>(tenorType.Id)).Returns(tenorType);
            var mapper = new EnergyTrading.MDM.Contracts.Mappers.ProductTenorTypeDetailsMapper(mockRepository.Object);

            var result = mapper.Map(source);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Product, product);
            Assert.AreEqual(result.TenorType, tenorType);
        }

        private static EntityId CreateEntityId(IEntityDetail product)
        {
            return new EntityId
            {
                Identifier = new NexusId
                {
                    Identifier = product.Id.ToString(CultureInfo.InvariantCulture),
                    IsNexusId = true
                },
            };
        }
    }
}
