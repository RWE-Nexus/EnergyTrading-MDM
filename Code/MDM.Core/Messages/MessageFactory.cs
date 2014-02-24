namespace EnergyTrading.MDM.Messages
{
    using System.Collections.Specialized;

    using EnergyTrading;
    using EnergyTrading.Extensions;
    using RWEST.Nexus.MDM.Contracts;

    public static class MessageFactory
    {
        public static MappingRequest MappingRequest(NameValueCollection collection)
        {
            var message = new MappingRequest();
            ParseMappingRequest(message, collection);

            return message;
        }

        public static CrossMappingRequest CrossMappingRequest(NameValueCollection collection)
        {
            var message = new CrossMappingRequest();

            ParseMappingRequest(message, collection);
            message.TargetSystemName = collection[QueryConstants.DestinationSystem];

            return message;
        }

        public static GetRequest GetRequest(NameValueCollection collection)
        {
            var message = new GetRequest();

            ParseReadRequest(message, collection);

            return message;
        }

        private static void ParseMappingRequest(MappingRequest message, NameValueCollection collection)
        {
            message.SystemName = collection[QueryConstants.SourceSystem];
            message.Identifier = collection[QueryConstants.MappingValue];

            ParseReadRequest(message, collection);
        }

        public static void ParseReadRequest(ReadRequest message, NameValueCollection collection)
        {
            var dt = collection.GetDateTime(QueryConstants.ValidAt);

            message.ValidAt = dt.HasValue ? dt.Value : SystemTime.UtcNow();
            message.ValidAtExists = dt.HasValue;
        }
    }
}