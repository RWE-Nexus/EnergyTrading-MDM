namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class TenorTypeDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            var source = new TenorTypeDetails
            {
                Name = "name",
                ShortName = "shortName",
            };
            var mapper = new EnergyTrading.MDM.Contracts.Mappers.TenorTypeDetailsMapper();

            var result = mapper.Map(source);

            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.ShortName, result.ShortName);
        }
    }
}
