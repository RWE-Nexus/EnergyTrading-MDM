namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnergyTrading.MDM.Test;

    [TestClass]
    public class BookDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<Book>();

            var mapper = new MDM.Mappers.BookDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.AreEqual(source.Name, result.Name);
        }
    }
}

    