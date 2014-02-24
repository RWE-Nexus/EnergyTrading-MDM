namespace EnergyTrading.MDM.Test.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading.MDM.Data.Search;
    using EnergyTrading.Test;

    [TestClass]
    public class when_a_request_is_made_for_all_entities_that_contain_a_specific_mapping_string_and_the_string_is_empty :
        SpecBase<SearchCommand<Party, PartyDetails, PartyMapping>>
    {
        private static DbSetRepository repository;

        private IList<int> entityIds;

        private string mappingValue;

        [TestMethod]
        public void should_return_no_entities_becuase_there_was_no_filter()
        {
            Assert.AreEqual(0, this.entityIds.Count);
        }

        protected override void Because_of()
        {
            this.entityIds = this.Sut.ExecuteMappingSearch("true", DateTime.Now, null);
        }

        protected override SearchCommand<Party, PartyDetails, PartyMapping> Establish_context()
        {
            var searchCommand = new SearchCommand<Party, PartyDetails, PartyMapping>(repository);
            return searchCommand;
        }

        protected override void Initialize()
        {
            repository = new DbSetRepository(new DbContextProvider(() => new MappingContext()));

            var party1 = ObjectMother.Create<Party>();
            var party2 = ObjectMother.Create<Party>();
            var system = new SourceSystem { Name = Guid.NewGuid().ToString() };
            this.mappingValue = Guid.NewGuid().ToString();

            party1.ProcessMapping(new PartyMapping { System = system, MappingValue = this.mappingValue });
            party1.ProcessMapping(new PartyMapping { System = system, MappingValue = Guid.NewGuid().ToString() });

            repository.Add(system);
            repository.Add(party1);
            repository.Add(party2);
            repository.Flush();
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_all_entities_that_contain_a_specific_mapping_string :
        SpecBase<SearchCommand<Party, PartyDetails, PartyMapping>>
    {
        private static DbSetRepository repository;

        private IList<int> entityIds;

        private string mappingValue;

        [TestMethod]
        public void should_return_an_entity_that_has_the_mapping_that_was_requested()
        {
            Assert.AreEqual(repository.FindOne<Party>(this.entityIds[0]).Mappings.Where(mapping => mapping.MappingValue == this.mappingValue).Count(), 1);
        }

        [TestMethod]
        public void should_return_the_correct_number_of_entities()
        {
            Assert.AreEqual(1, this.entityIds.Count);
        }

        protected override void Because_of()
        {
            this.entityIds = this.Sut.ExecuteMappingSearch("MappingValue = \"" + this.mappingValue + "\"", DateTime.Now, null);
        }

        protected override SearchCommand<Party, PartyDetails, PartyMapping> Establish_context()
        {
            var searchCommand = new SearchCommand<Party, PartyDetails, PartyMapping>(repository);
            return searchCommand;
        }

        protected override void Initialize()
        {
            repository = new DbSetRepository(new DbContextProvider(() => new MappingContext()));

            var party1 = ObjectMother.Create<Party>();
            var party2 = ObjectMother.Create<Party>();
            var system = new SourceSystem { Name = Guid.NewGuid().ToString() };
            this.mappingValue = Guid.NewGuid().ToString();

            party1.ProcessMapping(new PartyMapping { System = system, MappingValue = this.mappingValue });
            party1.ProcessMapping(new PartyMapping { System = system, MappingValue = Guid.NewGuid().ToString() });

            repository.Add(system);
            repository.Add(party1);
            repository.Add(party2);
            repository.Flush();
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_all_entities_that_contain_a_specific_mapping_string_with_max_results_specified :
        SpecBase<SearchCommand<Party, PartyDetails, PartyMapping>>
    {
        private static DbSetRepository repository;

        private IList<int> entities;

        private string mappingValue;

        private string mappingValue2;

        [TestMethod]
        public void should_return_an_entity_that_has_the_mapping_that_was_requested()
        {
            Assert.AreEqual(repository.FindOne<Party>(this.entities[0]).Mappings.Where(mapping => mapping.MappingValue == this.mappingValue || mapping.MappingValue == this.mappingValue2).Count(), 1);
        }

        [TestMethod]
        public void should_return_the_correct_number_of_entities()
        {
            Assert.AreEqual(1, this.entities.Count);
        }

        protected override void Because_of()
        {
            this.entities = this.Sut.ExecuteMappingSearch("MappingValue = \"" + this.mappingValue + "\" || MappingValue = \"" + this.mappingValue + "\"", DateTime.Now, 1);
        }

        protected override SearchCommand<Party, PartyDetails, PartyMapping> Establish_context()
        {
            var searchCommand = new SearchCommand<Party, PartyDetails, PartyMapping>(repository);
            return searchCommand;
        }

        protected override void Initialize()
        {
            repository = new DbSetRepository(new DbContextProvider(() => new MappingContext()));

            var party1 = ObjectMother.Create<Party>();
            var party2 = ObjectMother.Create<Party>();
            var system = new SourceSystem { Name = Guid.NewGuid().ToString() };
            this.mappingValue = Guid.NewGuid().ToString();
            this.mappingValue2 = Guid.NewGuid().ToString();

            party1.ProcessMapping(new PartyMapping { System = system, MappingValue = this.mappingValue });
            party2.ProcessMapping(new PartyMapping { System = system, MappingValue = this.mappingValue2 });

            repository.Add(system);
            repository.Add(party1);
            repository.Add(party2);
            repository.Flush();
        }
    }
}