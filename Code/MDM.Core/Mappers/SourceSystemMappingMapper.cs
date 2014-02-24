namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class SourceSystemMappingMapper: Mapper<EnergyTrading.MDM.SourceSystemMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public SourceSystemMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.SourceSystemMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}