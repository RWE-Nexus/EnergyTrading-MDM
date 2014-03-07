using EnergyTrading.Data;
using EnergyTrading.MDM.Data;

namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Mapping;

    /// <summary>
    ///
    /// </summary>
    public class CommodityFeeTypeDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.CommodityFeeTypeDetails, MDM.CommodityFeeType>
    {
        private readonly IRepository repository;

        public CommodityFeeTypeDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(RWEST.Nexus.MDM.Contracts.CommodityFeeTypeDetails source, MDM.CommodityFeeType destination)
        {
            destination.Commodity = this.repository.FindEntityByMapping<MDM.Commodity, CommodityMapping>(source.Commodity);
            destination.FeeType = this.repository.FindEntityByMapping<MDM.FeeType, MDM.FeeTypeMapping>(source.FeeType);
        }
    }
 } 