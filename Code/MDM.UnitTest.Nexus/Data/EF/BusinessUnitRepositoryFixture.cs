namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class BusinessUnitRepositoryFixture : DbSetRepositoryFixture<BusinessUnit>
    {
        protected override BusinessUnit Default()
        {
            var entity = ObjectMother.Create<BusinessUnit>();

            return entity;
        }
    }
}
