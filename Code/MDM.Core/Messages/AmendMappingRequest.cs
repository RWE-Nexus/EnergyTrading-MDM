namespace EnergyTrading.Mdm.Messages
{
    public class AmendMappingRequest
    {
        public int EntityId { get; set; }

        public int MappingId { get; set; }

        public EnergyTrading.Mdm.Contracts.MdmId Mapping { get; set; }

        public ulong Version { get; set; }
    }
}