namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class PortfolioHierarchyMappingRepositoryFixture : DbSetRepositoryFixture<PortfolioHierarchyMapping>
    {
        protected override PortfolioHierarchyMapping Default()
        {
            var entity = base.Default();
            entity.PortfolioHierarchy = ObjectMother.Create<PortfolioHierarchy>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}
