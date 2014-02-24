namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class CommodityMappingMapper: Mapper<EnergyTrading.MDM.CommodityMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public CommodityMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.CommodityMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}