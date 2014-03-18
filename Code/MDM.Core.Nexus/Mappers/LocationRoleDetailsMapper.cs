namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.MDM.Extensions;
    using LocationRole = EnergyTrading.MDM.LocationRole;

    public class LocationRoleDetailsMapper : Mapper<EnergyTrading.MDM.LocationRole, LocationRoleDetails>
    {
        public override void Map(EnergyTrading.MDM.LocationRole source, LocationRoleDetails destination)
        {
            destination.Location = source.Location.CreateNexusEntityId(() => source.Location.Name);
            destination.Type = source.Type.Name;
        }
    }
}