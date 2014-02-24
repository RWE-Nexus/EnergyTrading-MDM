namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class PortfolioMappingRepositoryFixture : DbSetRepositoryFixture<PortfolioMapping>
    {
        protected override PortfolioMapping Default()
        {
            var entity = base.Default();
            entity.Portfolio = ObjectMother.Create<Portfolio>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}
