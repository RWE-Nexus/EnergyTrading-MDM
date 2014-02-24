namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class BrokerCommodityMappingRepositoryFixture : DbSetRepositoryFixture<BrokerCommodityMapping>
    {
        protected override BrokerCommodityMapping Default()
        {
            var entity = base.Default();
            entity.BrokerCommodity = ObjectMother.Create<BrokerCommodity>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}
