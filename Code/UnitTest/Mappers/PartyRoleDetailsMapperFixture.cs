namespace EnergyTrading.MDM.Test.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PartyRoleDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<PartyRoleDetails>();

            var expected = new RWEST.Nexus.MDM.Contracts.PartyRoleDetails
            {
                Name = source.Name,
            };

            var mapper = new MDM.Mappers.PartyRoleDetailsMapper();

            // Act
            var candidate = mapper.Map(source);

            // Assert
            Check(expected, candidate);
        }
    }
}
