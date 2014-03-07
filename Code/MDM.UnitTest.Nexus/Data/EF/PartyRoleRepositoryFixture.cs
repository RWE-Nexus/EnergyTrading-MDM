namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class PartyRoleRepositoryFixture : DbSetRepositoryFixture<PartyRole>
    {
        protected override PartyRole Default()
        {
            var entity = ObjectMother.Create<PartyRole>();

            return entity;
        }
    }
}