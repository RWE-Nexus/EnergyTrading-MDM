namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class PartyOverrideMappingMapper: Mapper<EnergyTrading.MDM.PartyOverrideMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public PartyOverrideMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.PartyOverrideMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}