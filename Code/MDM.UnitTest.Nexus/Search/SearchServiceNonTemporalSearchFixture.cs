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

    public abstract class search_service_non_temporal_context : SpecBase<SearchService<Party, PartyDetails, PartyMapping>>
    {
        protected Mock<IQueryFactory> mockQueryFactory;

        protected Mock<ISearchCommand<Party, PartyDetails, PartyMapping>> mockSearchCommand;

        protected Search search;

        protected IList<Party> entitySearchResults = new List<Party>() { };

        protected IList<Party> results;

        protected override void Because_of()
        {
            this.results = this.Sut.NonTemporalSearch(this.search);
        }

        protected abstract Search CreateSearch();

        protected override SearchService<Party, PartyDetails, PartyMapping> Establish_context()
        {
            this.mockQueryFactory = new Mock<IQueryFactory>();
            this.mockSearchCommand = new Mock<ISearchCommand<Party, PartyDetails, PartyMapping>>();
            this.mockQueryFactory.Setup(factory => factory.CreateQuery(It.IsAny<Search>())).Returns("query string");
            this.mockSearchCommand.Setup(x => x.ExecuteNonTemporal(It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<int?>(), It.IsAny<string>())).
                Returns(this.entitySearchResults);

            this.search = this.CreateSearch();

            return new SearchService<Party, PartyDetails, PartyMapping>(
                this.mockQueryFactory.Object, this.mockSearchCommand.Object);
        }
    }

    [TestClass]
    public class when_the_search_service_is_asked_to_perform_a_non_temporal_search_with_max_results : search_service_non_temporal_context
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
            this.mockSearchCommand.Verify(command => command.ExecuteNonTemporal("query string", It.IsAny<DateTime?>(), 100, null));
        }

        [TestMethod]
        public void should_return_the_entity_ids_from_the_call_to_the_search_command()
        {
            Assert.AreEqual(this.entitySearchResults, this.results);
        }

        protected override Search CreateSearch()
        {
            this.s = SearchBuilder.CreateSearch().NotMultiPage().MaxPageSize(100);
            this.s.AddSearchCriteria(SearchCombinator.Or).AddCriteria("Name", SearchCondition.Equals, "TestParty");
            return this.s;
        }
    }

    [TestClass]
    public class when_the_search_orderby_option_is_set_for_a_non_temporal_search : search_service_non_temporal_context
    {
        [TestMethod]
        public void should_execute_non_temporal_search_with_orderby()
        {
            this.mockSearchCommand.Verify(
                command =>
                command.ExecuteNonTemporal(It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<int?>(), "orderby"));
        }

        protected override Search CreateSearch()
        {
            var search = SearchBuilder.CreateSearch().NotMultiPage().NoMaxPageSize();
            search.SearchOptions.OrderBy = "orderby";
            return search;
        }
    }

}