namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Data;

    public class SettlementContactDetailsMapper : Mapper<OpenNexus.MDM.Contracts.SettlementContactDetails, MDM.SettlementContact>
    {
        private IRepository repository;

        public SettlementContactDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(OpenNexus.MDM.Contracts.SettlementContactDetails source, MDM.SettlementContact destination)
        {
            destination.Name = source.Name;
            destination.PartyAccountabilityType = "SettlementContact";

            destination.SourcePerson = repository.FindEntityByMapping<MDM.Person, PersonMapping>(source.SourcePerson);
            destination.TargetPerson = repository.FindEntityByMapping<MDM.Person, PersonMapping>(source.TargetPerson);
            destination.SourceParty = repository.FindEntityByMapping<MDM.Party, PartyMapping>(source.SourceParty);
            destination.TargetParty = repository.FindEntityByMapping<MDM.Party, PartyMapping>(source.TargetParty);

            destination.CommodityInstrumentType = this.repository.FindEntityByMapping<MDM.CommodityInstrumentType, CommodityInstrumentTypeMapping>(source.CommodityInstrumentType);
            destination.Location = this.repository.FindEntityByMapping<MDM.Location, LocationMapping>(source.Location);
        }
    }
}