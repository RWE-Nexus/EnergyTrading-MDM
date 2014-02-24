namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnergyTrading.MDM.Test;

    [TestClass]
    public class AgreementDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<Agreement>();

            var mapper = new MDM.Mappers.AgreementDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.PaymentTerms, result.PaymentTerms);
        }
    }
}

    