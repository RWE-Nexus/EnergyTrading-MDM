namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class CommodityInstrumentTypeMappingMapper: Mapper<EnergyTrading.MDM.CommodityInstrumentTypeMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public CommodityInstrumentTypeMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.CommodityInstrumentTypeMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}