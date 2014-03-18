namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;

    /// <summary>
    /// Maps a <see cref="Vessel" /> to a <see cref="RWEST.Nexus.MDM.Contracts.VesselDetails" />
    /// </summary>
    public class VesselDetailsMapper : Mapper<EnergyTrading.MDM.Vessel, OpenNexus.MDM.Contracts.VesselDetails>
    {
        public override void Map(EnergyTrading.MDM.Vessel source, OpenNexus.MDM.Contracts.VesselDetails destination)
        {
            destination.Name = source.Name;
        }
    }
}