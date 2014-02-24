namespace EnergyTrading.MDM.Test.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PartyDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<PartyDetails>();

            var expected = new RWEST.Nexus.MDM.Contracts.PartyDetails
            {
                Name = source.Name,
                TelephoneNumber = source.Phone,
                FaxNumber = source.Fax,
                Role = source.Role
            };

            var mapper = new MDM.Mappers.PartyDetailsMapper();

            // Act
            var candidate = mapper.Map(source);

            // Assert
            Check(expected, candidate);
        }
    }
}