namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="CommodityFeeType" /> to a <see cref="RWEST.Nexus.MDM.Contracts.CommodityFeeTypeDetails" />
    /// </summary>
    public class CommodityFeeTypeDetailsMapper : Mapper<EnergyTrading.MDM.CommodityFeeType, OpenNexus.MDM.Contracts.CommodityFeeTypeDetails>
    {
        public override void Map(EnergyTrading.MDM.CommodityFeeType source, OpenNexus.MDM.Contracts.CommodityFeeTypeDetails destination)
        {
            destination.FeeType = source.FeeType.CreateNexusEntityId(() => source.FeeType.Name);
            destination.Commodity = source.Commodity.CreateNexusEntityId(() => source.Commodity.Name);
           
        }
    }
}