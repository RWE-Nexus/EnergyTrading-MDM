namespace MDM.ServiceHost.Wcf.Feeds
{
    using System;
    using System.ServiceModel.Channels;

    using EnergyTrading.Search;

    public interface IFeedFactory
    {
        Message CreateFeed<TContract>(SearchResultPage<TContract> cacheSearchResultPage, string entityName, Uri baseUri);
    }
}