namespace EnergyTrading.MDM.Test.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.MDM.Extensions;

    [TestClass]
    public class ExchangeDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<ExchangeDetails>();

            var expected = new RWEST.Nexus.MDM.Contracts.ExchangeDetails
            {
                Name = source.Name,
                Fax = source.Fax,
                Phone = source.Phone,
            };

            var mapper = new MDM.Mappers.ExchangeDetailsMapper();

            // Act
            var candidate = mapper.Map(source);

            // Assert
            Check(expected, candidate);
        }
    }
}

