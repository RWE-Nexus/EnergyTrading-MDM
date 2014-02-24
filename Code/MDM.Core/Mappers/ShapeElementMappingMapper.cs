namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class ShapeElementMappingMapper: Mapper<EnergyTrading.MDM.ShapeElementMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public ShapeElementMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.ShapeElementMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}