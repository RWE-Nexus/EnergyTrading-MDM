namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Data;

    using RWEST.Nexus.MDM.Contracts;

    public class BrokerRateDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.BrokerRateDetails, MDM.BrokerRateDetails>
    {
        private readonly IRepository repository;

        public BrokerRateDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(RWEST.Nexus.MDM.Contracts.BrokerRateDetails source, MDM.BrokerRateDetails destination)
        {
            destination.Broker = this.repository.FindEntityByMapping<MDM.Broker, PartyRoleMapping>(source.Broker);
            destination.Desk = this.repository.FindEntityByMapping<MDM.PartyRole, PartyRoleMapping>(source.Desk);
            destination.CommodityInstrumentType = this.repository.FindEntityByMapping<MDM.CommodityInstrumentType, CommodityInstrumentTypeMapping>(source.CommodityInstrumentType);
            destination.Location = this.repository.FindEntityByMapping<MDM.Location, LocationMapping>(source.Location);
            destination.ProductType = this.repository.FindEntityByMapping<MDM.ProductType, ProductTypeMapping>(source.ProductType);
            destination.PartyAction = (int)Enum.ToObject(typeof(PartyAction), source.PartyAction);
            destination.Rate = source.Rate;
            destination.RateType = source.RateType;
            destination.Currency = source.Currency;
        }
    }
}