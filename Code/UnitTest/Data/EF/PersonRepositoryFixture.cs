namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using System.Transactions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class PersonRepositoryFixture : DbSetRepositoryFixture<Person>
    {        
        [TestMethod]
        [TestCategory("Integration")]
        public void ParentChild()
        {
            var entity = this.Default();

            try
            {
                using (var scope = new TransactionScope())
                {
                    this.Repository.Add(entity);

                    this.Repository.Add(entity.Details[0]);
                    this.Repository.Add(entity.Mappings[0].System);
                    this.Repository.Add(entity.Mappings[0]);

                    this.Repository.Flush();
                    scope.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void ParentChild2()
        {
            var entity = new Person();
            var system = new SourceSystem { Name = "Fred" };

            try
            {
                using (var scope = new TransactionScope())
                {
                    this.Repository.Add(entity);
                    this.Repository.Add(system);
                    
                    this.Repository.Flush();
                    scope.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }

            //try
            //{
            //    using (var scope = new TransactionScope())
            //    {
            //        var d = new PersonDetails { Person = entity };
            //        entity.Details.Add(d);
            //        var m = new PersonMapping { System = system, Person = entity, MappingValue = "A" };
            //        entity.Mappings.Add(m);

            //        this.Repository.Save(entity);
            //        //this.Repository.Save(entity.Details[0]);
            //        //this.Repository.Save(entity.Mappings[0]);
            //        this.Repository.Flush();

            //        scope.Complete();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        protected override Person Default()
        {
            var entity = base.Default();
            var system = new SourceSystem { Name = "Fred" };

            var d = new PersonDetails { Person = entity };
            entity.Details.Add(d);

            var m = new PersonMapping { Person = entity, System = system, MappingValue = "A" };
            entity.Mappings.Add(m);

            return entity;
        }
    }
}