namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using NUnit.Framework;

    using EnergyTrading.Mdm;

    [TestFixture]
    public class SourceSystemMappingRepositoryFixture : DbSetRepositoryFixture<SourceSystemMapping>
    {
        protected override SourceSystemMapping Default()
        {
            var entity = base.Default();
            entity.SourceSystem = ObjectMother.Create<SourceSystem>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}
