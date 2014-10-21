﻿using System.Text;

namespace MDM.ServiceHost.WebApi.Infrastructure.Feeds
{
    using System;
    using System.IO;
    using System.Net.Http.Formatting;
    using System.ServiceModel.Syndication;
    using System.Threading.Tasks;
    using System.Xml;

    public class AtomSyndicationFeedFormatter : MediaTypeFormatter
    {
        public AtomSyndicationFeedFormatter()
        {
            SupportedEncodings.Add(new UTF8Encoding());
        }

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
                    var utf8NoBom = new UTF8Encoding(false);
                    var settings = new XmlWriterSettings
                    {
                        Encoding = utf8NoBom
                    };
                    using (XmlWriter writer = XmlWriter.Create(writeStream, settings))
                    {
                        var atomformatter = new Atom10FeedFormatter((SyndicationFeed) value);
                        atomformatter.WriteTo(writer);
                    }
                }
            });
        }
    }
}