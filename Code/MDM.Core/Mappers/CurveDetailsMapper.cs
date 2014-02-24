using EnergyTrading.MDM.Extensions;

namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="Curve" /> to a <see cref="RWEST.Nexus.MDM.Contracts.CurveDetails" />
    /// </summary>
    public class CurveDetailsMapper : Mapper<EnergyTrading.MDM.Curve, RWEST.Nexus.MDM.Contracts.CurveDetails>
    {
        public override void Map(EnergyTrading.MDM.Curve source, RWEST.Nexus.MDM.Contracts.CurveDetails destination)
        {
            destination.Name = source.Name;
            destination.CurveType = source.Type;
            destination.Currency = source.Currency;
            destination.Commodity = source.Commodity.CreateNexusEntityId(() => source.Commodity.Name);
            destination.CommodityUnit = source.CommodityUnit;
            destination.Location = source.Location.CreateNexusEntityId(() => source.Location.Name);
            destination.Originator = source.Originator.CreateNexusEntityId(() => source.Originator.LatestDetails.Name);
            destination.DefaultSpread = source.DefaultSpread;
        }
    }
}