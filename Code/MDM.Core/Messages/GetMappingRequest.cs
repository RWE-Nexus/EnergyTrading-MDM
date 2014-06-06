namespace EnergyTrading.Mdm.Messages
{
    public class GetMappingRequest : ReadRequest
    {
        public int EntityId { get; set; }

        public int MappingId { get; set; }
    }
}