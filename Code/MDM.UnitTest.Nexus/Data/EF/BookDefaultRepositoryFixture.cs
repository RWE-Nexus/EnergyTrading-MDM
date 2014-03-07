namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class BookDefaultRepositoryFixture : DbSetRepositoryFixture<BookDefault>
    {
        protected override BookDefault Default()
        {
            var entity = ObjectMother.Create<BookDefault>();

            return entity;
        }
    }
}
