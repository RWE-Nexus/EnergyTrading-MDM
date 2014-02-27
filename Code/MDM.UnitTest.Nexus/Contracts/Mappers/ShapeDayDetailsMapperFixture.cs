namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class ShapeDayDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            var shape = new MDM.Shape();
            var shapeElement = new MDM.ShapeElement();

            mockRepository.Setup(repository => repository.FindOne<MDM.Shape>(1)).Returns(shape);
            mockRepository.Setup(repository => repository.FindOne<MDM.ShapeElement>(1)).Returns(shapeElement);

            var source = new RWEST.Nexus.MDM.Contracts.ShapeDayDetails
                {
                    DayType = "dayType",
                    Shape = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
                    ShapeElement = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.ShapeDayDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.DayType, result.DayType);
            Assert.AreSame(shape, result.Shape);
            Assert.AreSame(shapeElement, result.ShapeElement);
        }
    }
}
