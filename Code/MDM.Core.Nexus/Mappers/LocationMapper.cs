namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class LocationMapper : Mapper<EnergyTrading.MDM.Location, OpenNexus.MDM.Contracts.Location>
    {
        public override void Map(EnergyTrading.MDM.Location source, OpenNexus.MDM.Contracts.Location destination)
        {
        }
    }
}
