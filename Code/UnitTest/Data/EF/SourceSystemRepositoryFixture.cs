namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class SourceSystemRepositoryFixture : DbSetRepositoryFixture<SourceSystem>
    {
        protected override SourceSystem Default()
        {
            var entity = ObjectMother.Create<SourceSystem>();

            return entity;
        }
    }
}
