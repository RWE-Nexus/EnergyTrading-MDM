namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class PartyOverrideMappingRepositoryFixture : DbSetRepositoryFixture<PartyOverrideMapping>
    {
        protected override PartyOverrideMapping Default()
        {
            var entity = base.Default();
            entity.PartyOverride = ObjectMother.Create<PartyOverride>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}
