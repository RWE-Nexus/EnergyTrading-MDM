namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class FeeTypeRepositoryFixture : DbSetRepositoryFixture<FeeType>
    {
        protected override FeeType Default()
        {
            var entity = ObjectMother.Create<FeeType>();

            return entity;
        }
    }
}
