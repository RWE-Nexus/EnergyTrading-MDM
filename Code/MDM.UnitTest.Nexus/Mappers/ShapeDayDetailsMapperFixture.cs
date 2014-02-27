using Microsoft.VisualStudio.TestTools.UnitTesting;
using RWEST.Nexus.MDM.Contracts;
using EnergyTrading.MDM.Extensions;

namespace EnergyTrading.MDM.Test.Mappers
{
    [TestClass]
    public class ShapeDayDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<ShapeDay>();

            var mapper = new MDM.Mappers.ShapeDayDetailsMapper();

            var expected = new RWEST.Nexus.MDM.Contracts.ShapeDayDetails()
            {
                Shape = source.Shape.CreateNexusEntityId(() => source.Shape.Name),
                DayType = source.DayType,
                ShapeElement = source.ShapeElement.CreateNexusEntityId(() => source.ShapeElement.Name)
            };

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.DayType, result.DayType);
            Assert.AreEqual(source.Shape.Name, result.Shape.Name);
            Assert.AreEqual(source.ShapeElement.Name, result.ShapeElement.Name);
        }
    }
}