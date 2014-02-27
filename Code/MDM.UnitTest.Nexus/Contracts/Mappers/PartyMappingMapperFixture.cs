namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PartyMappingMapperFixture : MappingMapperFixture
    {
        [TestMethod]
        public void Map()
        {
            this.Map<PartyMapping>();
        }
    }
}