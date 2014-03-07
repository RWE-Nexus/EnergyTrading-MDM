using Moq;
using RWEST.Nexus.Data;
using RWEST.Nexus.MDM.Contracts;

namespace RWEST.Nexus.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CurveDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            var fakeCommodity = new MDM.Commodity();
            var fakeLocation = new MDM.Location();

            mockRepository.Setup(repository => repository.FindOne<MDM.Location>(1)).Returns(fakeLocation);
            mockRepository.Setup(repository => repository.FindOne<MDM.Commodity>(1)).Returns(fakeCommodity);

            // Arrange
            var source = new MDM.Contracts.CurveDetails
                {
                    Name = "Test Curve",
                    CurveType = "Forward",
                    Currency = "GBP",
                    Commodity = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
                    CommodityUnit = "ton",
                    Location = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
                    DefaultSpread  = .005m
                };

            var mapper = new MDM.Contracts.Mappers.CurveDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
			// TODO_UnitTestGeneration_Curve - assert properties are mapped correctly
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(fakeCommodity, result.Commodity);
            Assert.AreEqual(fakeLocation, result.Location);

            Assert.AreEqual(source.Currency, result.Currency);
            Assert.AreEqual(source.CurveType, result.Type);
            Assert.AreEqual(source.CommodityUnit, result.CommodityUnit);
            Assert.AreEqual(source.DefaultSpread, result.DefaultSpread);
        }
    }
}
		