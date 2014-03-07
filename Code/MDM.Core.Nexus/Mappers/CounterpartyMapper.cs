namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class CounterpartyMapper : Mapper<EnergyTrading.MDM.Counterparty, RWEST.Nexus.MDM.Contracts.Counterparty>
    {
        public override void Map(EnergyTrading.MDM.Counterparty source, RWEST.Nexus.MDM.Contracts.Counterparty destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
        }
    }
}
