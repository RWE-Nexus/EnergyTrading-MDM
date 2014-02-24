namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class ShapeDayMappingMapper: Mapper<EnergyTrading.MDM.ShapeDayMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public ShapeDayMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.ShapeDayMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}