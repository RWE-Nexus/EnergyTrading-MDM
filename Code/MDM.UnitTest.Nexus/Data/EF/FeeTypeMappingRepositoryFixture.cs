namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class FeeTypeMappingRepositoryFixture : DbSetRepositoryFixture<FeeTypeMapping>
    {
        protected override FeeTypeMapping Default()
        {
            var entity = base.Default();
            entity.FeeType = ObjectMother.Create<FeeType>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}
