namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class PartyDetailsRepositoryFixture : DbSetRepositoryFixture<PartyDetails>
    {
        protected override PartyDetails Default()
        {
            var entity = base.Default();
            entity.Party = new Party();

            return entity;
        }
    }
}
