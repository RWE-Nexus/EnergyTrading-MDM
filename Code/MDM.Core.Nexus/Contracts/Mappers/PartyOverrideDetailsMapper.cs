namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Data;
    using EnergyTrading.Mapping;

    public class PartyOverrideDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.PartyOverrideDetails, MDM.PartyOverride>
    {
        private readonly IRepository repository;

        public PartyOverrideDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(RWEST.Nexus.MDM.Contracts.PartyOverrideDetails source, MDM.PartyOverride destination)
        {
            destination.Broker = this.repository.FindEntityByMapping<MDM.Broker, PartyRoleMapping>(source.Broker);
            destination.CommodityInstrumentType = this.repository.FindEntityByMapping<MDM.CommodityInstrumentType, CommodityInstrumentTypeMapping>(source.CommodityInstrumentType);
            destination.MappingValue = source.MappingValue;
            destination.Party = this.repository.FindEntityByMapping<MDM.Party, PartyMapping>(source.Party);
        }
    }
}