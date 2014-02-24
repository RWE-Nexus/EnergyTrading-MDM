namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class InstrumentTypeOverrideMappingMapper: Mapper<EnergyTrading.MDM.InstrumentTypeOverrideMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public InstrumentTypeOverrideMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.InstrumentTypeOverrideMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}