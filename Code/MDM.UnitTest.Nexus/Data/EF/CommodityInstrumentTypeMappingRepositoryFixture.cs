namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class CommodityInstrumentTypeMappingRepositoryFixture : DbSetRepositoryFixture<CommodityInstrumentTypeMapping>
    {
        protected override CommodityInstrumentTypeMapping Default()
        {
            var entity = base.Default();
            entity.CommodityInstrumentType = ObjectMother.Create<CommodityInstrumentType>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}
