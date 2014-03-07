namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class BookMappingRepositoryFixture : DbSetRepositoryFixture<BookMapping>
    {
        protected override BookMapping Default()
        {
            var entity = base.Default();
            entity.Book = ObjectMother.Create<Book>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}
