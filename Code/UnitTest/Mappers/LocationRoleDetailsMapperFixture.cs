namespace EnergyTrading.MDM.Test.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LocationRoleDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<LocationRole>();

            var mapper = new MDM.Mappers.LocationRoleDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Location.Id.ToString(), result.Location.Identifier.Identifier);
            Assert.AreEqual(source.Type.Name, result.Type);
        }
    }
}