using System;
using System.IO;
using System.Net.Http.Formatting;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace EnergyTrading.MDM.MappingService2.Infrastructure.Feeds
{
    public class AtomSyndicationFeedFormatter : MediaTypeFormatter
    {
        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof(SyndicationFeed);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, System.Net.Http.HttpContent content, System.Net.TransportContext transportContext)
        {
            return Task.Factory.StartNew(() =>
            {
                if (type == typeof(SyndicationFeed))
                {
                    using (XmlWriter writer = XmlWriter.Create(writeStream))
                    {
                        var atomformatter = new Atom10FeedFormatter((SyndicationFeed) value);
                        atomformatter.WriteTo(writer);
                    }
                }
            });
        }
    }
}