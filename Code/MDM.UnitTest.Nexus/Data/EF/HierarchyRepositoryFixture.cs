namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class HierarchyRepositoryFixture : DbSetRepositoryFixture<Hierarchy>
    {
        protected override Hierarchy Default()
        {
            var entity = ObjectMother.Create<Hierarchy>();

            return entity;
        }
    }
}
