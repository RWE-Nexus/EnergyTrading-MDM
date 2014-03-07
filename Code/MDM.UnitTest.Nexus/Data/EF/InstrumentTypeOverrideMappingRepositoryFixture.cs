namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class InstrumentTypeOverrideMappingRepositoryFixture : DbSetRepositoryFixture<InstrumentTypeOverrideMapping>
    {
        protected override InstrumentTypeOverrideMapping Default()
        {
            var entity = base.Default();
            entity.InstrumentTypeOverride = ObjectMother.Create<InstrumentTypeOverride>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}
