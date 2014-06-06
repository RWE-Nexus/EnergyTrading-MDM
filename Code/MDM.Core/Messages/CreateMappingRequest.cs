namespace EnergyTrading.Mdm.Messages
{
    public class CreateMappingRequest
    {
        public int EntityId { get; set; }

        public EnergyTrading.Mdm.Contracts.MdmId Mapping { get; set; }
    }
}