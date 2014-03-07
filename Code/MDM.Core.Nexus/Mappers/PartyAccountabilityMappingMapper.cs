namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class PartyAccountabilityMappingMapper: Mapper<EnergyTrading.MDM.PartyAccountabilityMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public PartyAccountabilityMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.PartyAccountabilityMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}