namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Extensions;
    using ShipperCode = EnergyTrading.MDM.ShipperCode;

    public class ShipperCodeDetailsMapper : Mapper<EnergyTrading.MDM.ShipperCode, ShipperCodeDetails>
    {
        public override void Map(EnergyTrading.MDM.ShipperCode source, ShipperCodeDetails destination)
        {
            destination.Location = source.Location.CreateNexusEntityId(() => source.Location.Name);
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
            destination.Code = source.Code;
        }
    }
}