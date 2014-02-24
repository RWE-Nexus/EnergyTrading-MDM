namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class LocationRoleMappingMapper: Mapper<EnergyTrading.MDM.LocationRoleMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public LocationRoleMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.LocationRoleMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}