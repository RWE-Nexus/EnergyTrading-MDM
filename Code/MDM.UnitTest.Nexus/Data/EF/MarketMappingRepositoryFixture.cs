namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class MarketMappingRepositoryFixture : DbSetRepositoryFixture<MarketMapping>
    {
        protected override MarketMapping Default()
        {
            var entity = base.Default();
            entity.Market = ObjectMother.Create<Market>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}
