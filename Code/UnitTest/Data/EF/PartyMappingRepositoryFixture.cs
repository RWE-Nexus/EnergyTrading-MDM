namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class PartyMappingRepositoryFixture : DbSetRepositoryFixture<PartyMapping>
    {
        protected override PartyMapping Default()
        {
            var entity = base.Default();
            entity.Party = new Party();
            entity.System = new SourceSystem { Name = "Mapping" };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}
