namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class PersonDetailsRepositoryFixture : DbSetRepositoryFixture<PersonDetails>
    {
        protected override PersonDetails Default()
        {
            var entity = base.Default();
            entity.Person = new Person();

            return entity;
        }
    }
}