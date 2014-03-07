namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class InstrumentTypeMappingMapper: Mapper<InstrumentTypeMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public InstrumentTypeMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.InstrumentTypeMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}