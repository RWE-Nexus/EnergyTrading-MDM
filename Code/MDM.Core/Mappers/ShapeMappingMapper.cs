namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class ShapeMappingMapper: Mapper<EnergyTrading.MDM.ShapeMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public ShapeMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.ShapeMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}