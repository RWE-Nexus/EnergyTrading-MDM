namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class BrokerMapper : Mapper<EnergyTrading.MDM.Broker, OpenNexus.MDM.Contracts.Broker>
    {
        public override void Map(EnergyTrading.MDM.Broker source, OpenNexus.MDM.Contracts.Broker destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
        }
    }
}
