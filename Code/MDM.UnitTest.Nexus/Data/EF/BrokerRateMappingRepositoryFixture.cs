namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class BrokerRateMappingRepositoryFixture : DbSetRepositoryFixture<BrokerRateMapping>
    {
        protected override BrokerRateMapping Default()
        {
            var entity = base.Default();
            entity.BrokerRate = ObjectMother.Create<BrokerRate>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}
