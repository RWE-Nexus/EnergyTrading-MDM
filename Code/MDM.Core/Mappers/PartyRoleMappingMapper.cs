namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
    using RWEST.Nexus.MDM;

    public class PartyRoleMappingMapper : Mapper<EnergyTrading.MDM.PartyRoleMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public PartyRoleMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.PartyRoleMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
