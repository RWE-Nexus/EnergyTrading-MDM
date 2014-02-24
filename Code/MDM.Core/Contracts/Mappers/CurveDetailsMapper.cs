using EnergyTrading.Data;
using EnergyTrading.MDM.Data;

namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Mapping;

    /// <summary>
	///
	/// </summary>
    public class CurveDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.CurveDetails, MDM.Curve>
    {
        private readonly IRepository repository;

        public CurveDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(RWEST.Nexus.MDM.Contracts.CurveDetails source, MDM.Curve destination)
        {
            destination.Name = source.Name;
            destination.Type= source.CurveType;
            destination.Currency = source.Currency;
            destination.Commodity = this.repository.FindEntityByMapping<MDM.Commodity, CommodityMapping>(source.Commodity);
            destination.CommodityUnit = source.CommodityUnit;
            destination.Location = this.repository.FindEntityByMapping<MDM.Location, LocationMapping>(source.Location);
            destination.Originator = this.repository.FindEntityByMapping<MDM.Party, PartyMapping>(source.Originator);
            destination.DefaultSpread = source.DefaultSpread;
        }
    }
}