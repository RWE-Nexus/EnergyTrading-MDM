namespace RWEST.Nexus.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class CurveMappingRepositoryFixture : DbSetRepositoryFixture<CurveMapping>
    {
        protected override CurveMapping Default()
        {
            var entity = base.Default();
            entity.Curve = ObjectMother.Create<Curve>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}
