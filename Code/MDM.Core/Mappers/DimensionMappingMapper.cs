namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class DimensionMappingMapper: Mapper<EnergyTrading.MDM.DimensionMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public DimensionMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.DimensionMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}