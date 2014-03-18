namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Mapping;

    public class LocationDetailsMapper : Mapper<EnergyTrading.MDM.Location, OpenNexus.MDM.Contracts.LocationDetails>
    {
        public override void Map(EnergyTrading.MDM.Location source, OpenNexus.MDM.Contracts.LocationDetails destination)
        {
            destination.Type = source.Type ?? string.Empty;
            destination.Name = source.Name;
            destination.Parent = source.Parent.CreateNexusEntityId(() => source.Parent.Name);
        }
    }
}		