namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class ExchangeMapper : Mapper<EnergyTrading.MDM.Exchange, OpenNexus.MDM.Contracts.Exchange>
    {
        public override void Map(EnergyTrading.MDM.Exchange source, OpenNexus.MDM.Contracts.Exchange destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
        }
    }
}
