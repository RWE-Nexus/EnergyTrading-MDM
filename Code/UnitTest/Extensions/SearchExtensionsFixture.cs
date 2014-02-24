namespace EnergyTrading.MDM.Test.Extensions
{
    using System.Web;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Search;

    [TestClass]
    public class SearchExtensionsFixture
    {
        [TestMethod]
        public void KeysForSameSearchAreTheSame()
        {
            var search = SearchBuilder.CreateSearch();
            search.AddSearchCriteria(SearchCombinator.And)
                    .AddCriteria("TargetPerson.Id", SearchCondition.Equals, "1", true)
                    .AddCriteria("PartyAccountabilityType", SearchCondition.Equals, "PartyAccountability", false);
            var key1 = search.ToKey<PartyAccountability>();
            var key2 = search.ToKey<PartyAccountability>();
            Assert.AreEqual(key1, key2);
        }

        [TestMethod]
        public void KeysForSameSearchButDifferentEntitiesAreDifferent()
        {
            var search = SearchBuilder.CreateSearch();
            search.AddSearchCriteria(SearchCombinator.And)
                  .AddCriteria("Name", SearchCondition.Equals, "bing", true);
            var key1 = search.ToKey<Person>();
            var key2 = search.ToKey<Party>();
            Assert.AreNotEqual(key1, key2);
        }

        [TestMethod]
        public void SearchKeysAreReversible()
        {
            var search = SearchBuilder.CreateSearch();
            search.AddSearchCriteria(SearchCombinator.And)
                    .AddCriteria("TargetPerson.Id", SearchCondition.Equals, "1", true)
                    .AddCriteria("PartyAccountabilityType", SearchCondition.Equals, "PartyAccountability", false);
            var key1 = search.ToKey<PartyAccountability>();
            var candidate = key1.ToSearch<PartyAccountability>();
            Assert.AreEqual(candidate.SearchFields.Combinator, SearchCombinator.And);
            Assert.AreEqual(candidate.SearchFields.Criterias[0].Criteria[0].Field, "TargetPerson.Id");
            Assert.AreEqual(candidate.SearchFields.Criterias[0].Criteria[1].Field, "PartyAccountabilityType");
            Assert.AreEqual(candidate.SearchFields.Criterias[0].Criteria[0].Condition, SearchCondition.Equals);
            Assert.AreEqual(candidate.SearchFields.Criterias[0].Criteria[1].Condition, SearchCondition.Equals);
            Assert.AreEqual(candidate.SearchFields.Criterias[0].Criteria[0].ComparisonValue, "1");
            Assert.AreEqual(candidate.SearchFields.Criterias[0].Criteria[1].ComparisonValue, "PartyAccountability");
        }

        [TestMethod]
        public void KeyIsNotAffectedByUrlEncoding()
        {
            var search = SearchBuilder.CreateSearch();
            search.AddSearchCriteria(SearchCombinator.And)
                    .AddCriteria("TargetPerson.Id", SearchCondition.Equals, "1", true)
                    .AddCriteria("PartyAccountabilityType", SearchCondition.Equals, "PartyAccountability", false);
            var key1 = search.ToKey<PartyAccountability>();
            var urlEncodedKey = HttpUtility.UrlEncode(key1);
            Assert.AreEqual(key1, urlEncodedKey);
        }
    }
}