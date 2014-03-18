namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class CounterpartyMapper : Mapper<EnergyTrading.MDM.Counterparty, OpenNexus.MDM.Contracts.Counterparty>
    {
        public override void Map(EnergyTrading.MDM.Counterparty source, OpenNexus.MDM.Contracts.Counterparty destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
        }
    }
}
