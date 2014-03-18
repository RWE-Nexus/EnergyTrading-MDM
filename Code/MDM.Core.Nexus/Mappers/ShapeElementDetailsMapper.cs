namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="ShapeElement" /> to a <see cref="RWEST.Nexus.MDM.Contracts.ShapeElementDetails" />
    /// </summary>
    public class ShapeElementDetailsMapper : Mapper<EnergyTrading.MDM.ShapeElement, OpenNexus.MDM.Contracts.ShapeElementDetails>
    {
        public override void Map(EnergyTrading.MDM.ShapeElement source, OpenNexus.MDM.Contracts.ShapeElementDetails destination)
        {
            destination.Name = source.Name;
            destination.Period = source.Period.ToContract();
        }
    }
}