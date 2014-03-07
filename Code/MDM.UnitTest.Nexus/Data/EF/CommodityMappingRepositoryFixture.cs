namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class CommodityMappingRepositoryFixture : DbSetRepositoryFixture<CommodityMapping>
    {
        protected override CommodityMapping Default()
        {
            var entity = base.Default();
            entity.Commodity = ObjectMother.Create<Commodity>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}
