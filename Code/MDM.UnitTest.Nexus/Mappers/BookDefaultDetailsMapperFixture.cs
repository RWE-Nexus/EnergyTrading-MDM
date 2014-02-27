namespace EnergyTrading.MDM.Test.Mappers
{
    using System.Globalization;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnergyTrading.MDM.Test;

    [TestClass]
    public class BookDefaultDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<BookDefault>();

            var mapper = new MDM.Mappers.BookDefaultDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.Book.Id.ToString(CultureInfo.InvariantCulture), result.Book.Identifier.Identifier);
            Assert.AreEqual(source.Desk.Id.ToString(CultureInfo.InvariantCulture), result.Desk.Identifier.Identifier);
            Assert.AreEqual(source.Trader.Id.ToString(CultureInfo.InvariantCulture), result.Trader.Identifier.Identifier);
            Assert.AreEqual(source.GfProductMapping, result.GfProductMapping);
            Assert.AreEqual(source.DefaultType, result.DefaultType);
            Assert.AreEqual(source.PartyRole.Id.ToString(CultureInfo.InvariantCulture), result.PartyRole.Identifier.Identifier);
        }
    }
}

    