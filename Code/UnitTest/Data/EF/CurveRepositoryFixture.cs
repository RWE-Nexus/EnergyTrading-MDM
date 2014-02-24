namespace RWEST.Nexus.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class CurveRepositoryFixture : DbSetRepositoryFixture<Curve>
    {
        protected override Curve Default()
        {
            var entity = ObjectMother.Create<Curve>();

            return entity;
        }
    }
}
