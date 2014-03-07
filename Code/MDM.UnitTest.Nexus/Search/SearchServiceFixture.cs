namespace EnergyTrading.MDM.Test.Search
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Data.Search;
    using EnergyTrading.Search;
    using EnergyTrading.Test;

    public abstract class search_service_context : SpecBase<SearchService<Party, PartyDetails, PartyMapping>>
    {
        protected Mock<IQueryFactory> mockQueryFactory;

        protected Mock<ISearchCommand<Party, PartyDetails, PartyMapping>> mockSearchCommand;

        protected Search search;

        protected IList<int> mappingSearchResults = new List<int>() { 1, 2 };

        protected IList<int> entitySearchResults = new List<int>() { 3, 4 };

        protected IList<int> results;

        protected override void Because_of()
        {
            this.results = this.Sut.Search(this.search);
        }

        protected abstract Search CreateSearch();

        protected override SearchService<Party, PartyDetails, PartyMapping> Establish_context()
        {
            this.mockQueryFactory = new Mock<IQueryFactory>();
            this.mockSearchCommand = new Mock<ISearchCommand<Party, PartyDetails, PartyMapping>>();
            this.mockQueryFactory.Setup(factory => factory.CreateQuery(It.IsAny<Search>())).Returns("query string");
            this.mockSearchCommand.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<int?>(), It.IsAny<string>())).
                Returns(this.entitySearchResults);
            this.mockSearchCommand.Setup(x => x.ExecuteMappingSearch(It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<int?>())).
                Returns(this.mappingSearchResults);

            this.search = this.CreateSearch();

            return new SearchService<Party, PartyDetails, PartyMapping>(
                this.mockQueryFactory.Object, this.mockSearchCommand.Object);
        }
    }

    [TestClass]
    public class when_the_search_service_is_asked_to_perform_a_standard_search : search_service_context
    {
        private Search s;

        [TestMethod]
        public void should_create_a_query_for_the_search_criteria()
        {
            this.mockQueryFactory.Verify(factory => factory.CreateQuery(this.search));
        }

        [TestMethod]
        public void should_create_an_entity_search_command()
        {
            this.mockSearchCommand.Verify(command => command.Execute("query string", It.IsAny<DateTime?>(), this.s.SearchOptions.ResultsPerPage, It.IsAny<string>()));
        }

        [TestMethod]
        public void should_return_the_entity_ids_from_the_call_to_the_search_command()
        {
            Assert.AreEqual(this.entitySearchResults, this.results);
        }

        protected override Search CreateSearch()
        {
            this.s = SearchBuilder.CreateSearch().NotMultiPage().NoMaxPageSize();
            this.s.AddSearchCriteria(SearchCombinator.Or).AddCriteria("Name", SearchCondition.Equals, "TestParty");
            return this.s;
        }
    }

    [TestClass]
    public class when_the_search_orderby_option_is_set : search_service_context
    {
        [TestMethod]
        public void should_execute_standard_search_with_orderby()
        {
            this.mockSearchCommand.Verify(command => command.Execute(It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<int?>(), "orderby"));
        }

        protected override Search CreateSearch()
        {
            var search = SearchBuilder.CreateSearch().NotMultiPage().NoMaxPageSize();
            search.SearchOptions.OrderBy = "orderby";
            return search;
        }
    }

    [TestClass]
    public class when_the_search_service_is_asked_to_perform_a_mapping_search : search_service_context
    {
        private Search s;

        [TestMethod]
        public void should_create_a_query_for_the_search_criteria()
        {
            this.mockQueryFactory.Verify(factory => factory.CreateQuery(this.search));
        }

        [TestMethod]
        public void should_create_an_entity_search_command()
        {
            this.mockSearchCommand.Verify(command => command.ExecuteMappingSearch("query string", It.IsAny<DateTime?>(), search.SearchOptions.ResultsPerPage));
        }

        [TestMethod]
        public void should_return_the_entity_ids_from_the_call_to_the_search_command()
        {
            Assert.AreEqual(this.mappingSearchResults, this.results);
        }

        protected override Search CreateSearch()
        {
            this.s = SearchBuilder.CreateSearch(isMappingSearch: true).NotMultiPage().NoMaxPageSize();
            this.s.AddSearchCriteria(SearchCombinator.Or).AddCriteria("Name", SearchCondition.Equals, "TestParty");
            return this.s;
        }
    }
}