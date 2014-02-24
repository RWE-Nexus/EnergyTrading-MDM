namespace EnergyTrading.MDM.Messages
{
    public class AmendMappingRequest
    {
        public int EntityId { get; set; }

        public int MappingId { get; set; }

        public RWEST.Nexus.MDM.Contracts.NexusId Mapping { get; set; }

        public long Version { get; set; }
    }
}