namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class BrokerRateMapper : Mapper<EnergyTrading.MDM.BrokerRate, RWEST.Nexus.MDM.Contracts.BrokerRate>
    {
        public override void Map(EnergyTrading.MDM.BrokerRate source, RWEST.Nexus.MDM.Contracts.BrokerRate destination)
        {
        }
    }
}
