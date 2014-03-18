namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Data;
    using EnergyTrading.Mapping;

    public class ShipperCodeDetailsMapper : Mapper<OpenNexus.MDM.Contracts.ShipperCodeDetails, MDM.ShipperCode>
    {
        private readonly IRepository repository;

        public ShipperCodeDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(OpenNexus.MDM.Contracts.ShipperCodeDetails source, MDM.ShipperCode destination)
        {
            destination.Location = this.repository.FindEntityByMapping<MDM.Location, LocationMapping>(source.Location);
            destination.Party = this.repository.FindEntityByMapping<MDM.Party, PartyMapping>(source.Party);
            destination.Code = source.Code;
        }
    }
}
