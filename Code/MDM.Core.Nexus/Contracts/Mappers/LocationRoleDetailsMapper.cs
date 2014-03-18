namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.MDM.Data;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;

    public class LocationRoleDetailsMapper : Mapper<OpenNexus.MDM.Contracts.LocationRoleDetails, MDM.LocationRole>
    {
        private readonly IRepository repository;

        public LocationRoleDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(OpenNexus.MDM.Contracts.LocationRoleDetails source, MDM.LocationRole destination)
        {
            destination.Type = this.repository.LocationRoleTypeByName(source.Type);
            destination.Location = this.repository.FindEntityByMapping<MDM.Location, LocationMapping>(source.Location);
        }
    }
}