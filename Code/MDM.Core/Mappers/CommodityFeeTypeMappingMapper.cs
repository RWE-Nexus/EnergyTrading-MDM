namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class CommodityFeeTypeMappingMapper: Mapper<EnergyTrading.MDM.CommodityFeeTypeMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public CommodityFeeTypeMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.CommodityFeeTypeMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}