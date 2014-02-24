namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class CommodityInstrumentTypeRepositoryFixture : DbSetRepositoryFixture<CommodityInstrumentType>
    {
        protected override CommodityInstrumentType Default()
        {
            var entity = ObjectMother.Create<CommodityInstrumentType>();

            return entity;
        }
    }
}
