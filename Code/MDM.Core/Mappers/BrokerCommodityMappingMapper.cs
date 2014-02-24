namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class BrokerCommodityMappingMapper: Mapper<EnergyTrading.MDM.BrokerCommodityMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public BrokerCommodityMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.BrokerCommodityMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}