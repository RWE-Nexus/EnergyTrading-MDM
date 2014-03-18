namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.MDM.Data;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;

    public class LocationDetailsMapper : Mapper<OpenNexus.MDM.Contracts.LocationDetails, MDM.Location>
    {
        private readonly IRepository repository;

        public LocationDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(OpenNexus.MDM.Contracts.LocationDetails source, MDM.Location destination)
        {
            destination.Name = source.Name;
            var referenceData = this.repository.LocationTypeByName(source.Type);

            // TODO: Raise an exception because this location type doesn't exist?
            destination.Type = referenceData != null ? referenceData.Value : null;
            destination.Parent = this.repository.FindEntityByMapping<MDM.Location, LocationMapping>(source.Parent);
        }
    }
}