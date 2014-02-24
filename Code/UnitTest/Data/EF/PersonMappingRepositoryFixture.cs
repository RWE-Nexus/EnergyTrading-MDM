namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class PersonMappingRepositoryFixture : DbSetRepositoryFixture<PersonMapping>
    {
        protected override PersonMapping Default()
        {
            var entity = base.Default();
            entity.Person = new Person();
            entity.System = new SourceSystem { Name = "Mapping" };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}