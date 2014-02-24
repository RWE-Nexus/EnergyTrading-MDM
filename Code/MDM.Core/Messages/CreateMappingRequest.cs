namespace EnergyTrading.MDM.Messages
{
    public class CreateMappingRequest
    {
        public int EntityId { get; set; }

        public RWEST.Nexus.MDM.Contracts.NexusId Mapping { get; set; }
    }
}