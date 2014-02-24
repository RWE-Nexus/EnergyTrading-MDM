namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class PortfolioHierarchyRepositoryFixture : DbSetRepositoryFixture<PortfolioHierarchy>
    {
        protected override PortfolioHierarchy Default()
        {
            var entity = ObjectMother.Create<PortfolioHierarchy>();

            return entity;
        }
    }
}
