namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;

    /// <summary>
    /// Maps a <see cref="Shape" /> to a <see cref="RWEST.Nexus.MDM.Contracts.ShapeDetails" />
    /// </summary>
    public class ShapeDetailsMapper : Mapper<EnergyTrading.MDM.Shape, RWEST.Nexus.MDM.Contracts.ShapeDetails>
    {
        public override void Map(EnergyTrading.MDM.Shape source, RWEST.Nexus.MDM.Contracts.ShapeDetails destination)
        {
            destination.Name = source.Name;
        }
    }
}