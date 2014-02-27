namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class BookRepositoryFixture : DbSetRepositoryFixture<Book>
    {
        protected override Book Default()
        {
            var entity = ObjectMother.Create<Book>();

            return entity;
        }
    }
}
