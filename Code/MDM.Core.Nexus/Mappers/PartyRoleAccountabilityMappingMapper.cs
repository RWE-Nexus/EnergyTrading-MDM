namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using RWEST.Nexus.MDM.Contracts;

    public class PartyRoleAccountabilityMappingMapper : Mapper<EnergyTrading.MDM.PartyRoleAccountabilityMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public PartyRoleAccountabilityMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.PartyRoleAccountabilityMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}