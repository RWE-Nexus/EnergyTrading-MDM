namespace EnergyTrading.MDM.Test.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading.MDM.Data.Search;
    using EnergyTrading.MDM.Test.Data.EF;
    using EnergyTrading.Test;

    [TestClass]
    public class when_a_request_is_made_for_all_entities :
        SpecBase<SearchCommand<Party, PartyDetails, PartyMapping>>
    {
        private static DbSetRepository repository;

        private IList<int> entityIds;
        private Party party1;
        private Party party2;

        [TestMethod]
        public void should_return_the_expected_entity()
        {
            Assert.AreEqual(repository.FindOne<Party>(this.entityIds[0]).Details[0].Name, this.party1.Details[0].Name);
        }

        [TestMethod]
        [Ignore]
        public void should_return_the_correct_number_of_entities()
        {
            Assert.AreEqual(1, this.entityIds.Count);
        }

        protected override void Because_of()
        {
            this.entityIds = this.Sut.Execute("Name = \"" + this.party1.Details[0].Name + "\"", DateTime.Now, null, "Name");
        }

        protected override SearchCommand<Party, PartyDetails, PartyMapping> Establish_context()
        {
            var searchCommand = new SearchCommand<Party, PartyDetails, PartyMapping>(repository);
            return searchCommand;
        }

        protected override void Initialize()
        {
            repository = new DbSetRepository(new DbContextProvider(() => new MappingContext()));
            var z = new Zapper(repository);
            z.Zap();

            this.party1 = ObjectMother.Create<Party>();
            this.party2 = ObjectMother.Create<Party>();
            repository.Add(this.party1);
            repository.Add(this.party2);
            repository.Flush();
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_all_entities_with_max_results_specified :
        SpecBase<SearchCommand<Party, PartyDetails, PartyMapping>>
    {
        private static DbSetRepository repository;

        private IList<int> entityIds;
        private Party party1;
        private Party party2;

        [TestMethod]
        public void should_return_one_of_the_expected_entity()
        {
            Assert.IsTrue(this.entityIds.Contains(this.party1.Id) || this.entityIds.Contains(this.party2.Id));
        }

        [TestMethod]
        [Ignore]
        public void should_return_the_capped_number_of_max_results()
        {
            Assert.AreEqual(1, this.entityIds.Count);
        }

        protected override void Because_of()
        {
            this.entityIds = this.Sut.Execute("Name = \"" + this.party1.Details[0].Name + "\" OR Name = \"" + this.party2.Details[0].Name + "\"", DateTime.Now, 1, "Name");
        }

        protected override SearchCommand<Party, PartyDetails, PartyMapping> Establish_context()
        {
            var searchCommand = new SearchCommand<Party, PartyDetails, PartyMapping>(repository);
            return searchCommand;
        }

        protected override void Initialize()
        {
            repository = new DbSetRepository(new DbContextProvider(() => new MappingContext()));
            var z = new Zapper(repository);
            z.Zap();

            this.party1 = ObjectMother.Create<Party>();
            this.party2 = ObjectMother.Create<Party>();
            repository.Add(this.party1);
            repository.Add(this.party2);
            repository.Flush();
        }
    }
}