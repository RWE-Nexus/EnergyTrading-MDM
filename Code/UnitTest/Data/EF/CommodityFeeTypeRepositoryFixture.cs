namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class CommodityFeeTypeRepositoryFixture : DbSetRepositoryFixture<CommodityFeeType>
    {
        protected override CommodityFeeType Default()
        {
            var entity = ObjectMother.Create<CommodityFeeType>();

            return entity;
        }
    }
}
