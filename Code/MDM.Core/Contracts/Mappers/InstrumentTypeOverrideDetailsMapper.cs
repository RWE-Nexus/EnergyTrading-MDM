namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Data;
    using EnergyTrading.Mapping;

    public class InstrumentTypeOverrideDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.InstrumentTypeOverrideDetails, MDM.InstrumentTypeOverride>
    {
        private readonly IRepository repository;

        public InstrumentTypeOverrideDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(RWEST.Nexus.MDM.Contracts.InstrumentTypeOverrideDetails source, MDM.InstrumentTypeOverride destination)
        {
            destination.Name = source.Name;
            destination.ProductType = this.repository.FindEntityByMapping<MDM.ProductType, ProductTypeMapping>(source.ProductType);
            destination.Broker = this.repository.FindEntityByMapping<MDM.Broker, PartyRoleMapping>(source.Broker);
            destination.CommodityInstrumentType = this.repository.FindEntityByMapping<MDM.CommodityInstrumentType, CommodityInstrumentTypeMapping>(source.CommodityInstrumentType);
            destination.InstrumentSubType = source.InstrumentSubType;
            destination.ProductTenorType = this.repository.FindEntityByMapping<MDM.ProductTenorType, ProductTenorTypeMapping>(source.ProductTenorType);
        }
    }
}