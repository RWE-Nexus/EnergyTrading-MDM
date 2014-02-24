namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class BrokerMapper : Mapper<EnergyTrading.MDM.Broker, RWEST.Nexus.MDM.Contracts.Broker>
    {
        public override void Map(EnergyTrading.MDM.Broker source, RWEST.Nexus.MDM.Contracts.Broker destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
        }
    }
}
