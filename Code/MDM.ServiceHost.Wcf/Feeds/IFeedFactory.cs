namespace EnergyTrading.Mdm.ServiceHost.Wcf.Feeds
{
    using System;
    using System.ServiceModel.Channels;

    using EnergyTrading.Search;

    /// <summary>
    /// Converts search results into an atom feed.
    /// </summary>
    public interface IFeedFactory
    {
        /// <summary>
        /// Create an atom feed message from search results
        /// </summary>
        /// <typeparam name="TContract">Contract to use</typeparam>
        /// <param name="searchResultPage">Results to convert</param>
        /// <param name="entityName">Entity name to use</param>
        /// <param name="baseUri">Base URI to use.</param>
        /// <returns></returns>
        Message CreateFeed<TContract>(SearchResultPage<TContract> searchResultPage, string entityName, Uri baseUri);
    }
}