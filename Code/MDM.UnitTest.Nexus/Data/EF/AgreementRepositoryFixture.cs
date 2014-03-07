namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class AgreementRepositoryFixture : DbSetRepositoryFixture<Agreement>
    {
        protected override Agreement Default()
        {
            var entity = ObjectMother.Create<Agreement>();

            return entity;
        }
    }
}
