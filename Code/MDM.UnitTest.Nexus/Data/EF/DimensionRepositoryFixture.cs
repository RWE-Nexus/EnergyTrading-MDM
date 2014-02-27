namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class DimensionRepositoryFixture : DbSetRepositoryFixture<Dimension>
    {
        protected override Dimension Default()
        {
            var entity = ObjectMother.Create<Dimension>();

            return entity;
        }
    }
}
