namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class TenorTypeRepositoryFixture : DbSetRepositoryFixture<TenorType>
    {
        protected override TenorType Default()
        {
            var entity = ObjectMother.Create<TenorType>();

            return entity;
        }
    }
}
