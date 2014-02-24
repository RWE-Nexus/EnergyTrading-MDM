namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class PartyOverrideRepositoryFixture : DbSetRepositoryFixture<PartyOverride>
    {
        protected override PartyOverride Default()
        {
            var entity = ObjectMother.Create<PartyOverride>();

            return entity;
        }
    }
}
