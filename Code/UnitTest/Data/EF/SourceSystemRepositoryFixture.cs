namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Mdm;

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
