namespace EnergyTrading.MDM.Extensions
{
    using System;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Messages;

    public static class MappingRequestExtensions
    {
        public static bool IsNexusMappingRequest(this MappingRequest mappingRequest)
        {
            if (string.IsNullOrWhiteSpace(mappingRequest.SystemName))
            {
                return false;
            }

            return mappingRequest.SystemName.Trim().ToLower() == SourceSystemNames.Nexus.ToLower();
        }

        public static bool HasNumericIdentifier(this MappingRequest mappingRequest)
        {
            int id;
            return int.TryParse(mappingRequest.Identifier, out id);
        }

        public static GetRequest ToGetRequest(this MappingRequest mappingRequest)
        {
            int id;
            if (int.TryParse(mappingRequest.Identifier, out id))
            {
                return new GetRequest()
                    {
                        EntityId = int.Parse(mappingRequest.Identifier),
                        ValidAt = mappingRequest.ValidAt,
                        ValidAtExists = mappingRequest.ValidAtExists,
                        Version = mappingRequest.Version
                    };
            }
            else
            {
                throw new ArgumentException(string.Format("Invalid Nexus identifier: {0}", mappingRequest.Identifier));
            }
        }
    }
}