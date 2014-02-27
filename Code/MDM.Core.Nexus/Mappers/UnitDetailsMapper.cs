namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="Unit" /> to a <see cref="RWEST.Nexus.MDM.Contracts.UnitDetails" />
    /// </summary>
    public class UnitDetailsMapper : Mapper<EnergyTrading.MDM.Unit, RWEST.Nexus.MDM.Contracts.UnitDetails>
    {
        public override void Map(EnergyTrading.MDM.Unit source, RWEST.Nexus.MDM.Contracts.UnitDetails destination)
        {
            destination.Name = source.Name;
            destination.Description = source.Description;
            destination.Dimension = source.Dimension.CreateNexusEntityId(() => source.Dimension.Name);
            destination.ConversionConstant = source.ConversionConstant;
            destination.ConversionFactor = source.ConversionFactor;
            destination.Symbol = source.Symbol;
        }
    }
}