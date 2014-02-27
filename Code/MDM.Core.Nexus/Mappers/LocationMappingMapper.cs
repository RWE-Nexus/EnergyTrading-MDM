namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class LocationMappingMapper: Mapper<EnergyTrading.MDM.LocationMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public LocationMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.LocationMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}