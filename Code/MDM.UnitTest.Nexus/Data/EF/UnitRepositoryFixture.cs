namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class UnitRepositoryFixture : DbSetRepositoryFixture<Unit>
    {
        protected override Unit Default()
        {
            var entity = ObjectMother.Create<Unit>();

            return entity;
        }
    }
}
