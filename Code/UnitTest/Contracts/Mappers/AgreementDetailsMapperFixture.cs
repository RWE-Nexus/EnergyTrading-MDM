namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AgreementDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = new RWEST.Nexus.MDM.Contracts.AgreementDetails
                {
                    Name = "test",
                    PaymentTerms = "payment terms"
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.AgreementDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.PaymentTerms, result.PaymentTerms);
        }
    }
}
        