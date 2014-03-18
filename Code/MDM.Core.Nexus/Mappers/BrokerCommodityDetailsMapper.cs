using EnergyTrading.MDM.Extensions;

namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;

    /// <summary>
    /// Maps a <see cref="BrokerCommodity" /> to a <see cref="RWEST.Nexus.MDM.Contracts.BrokerCommodityDetails" />
    /// </summary>
    public class BrokerCommodityDetailsMapper : Mapper<EnergyTrading.MDM.BrokerCommodity, OpenNexus.MDM.Contracts.BrokerCommodityDetails>
    {
        public override void Map(EnergyTrading.MDM.BrokerCommodity source, OpenNexus.MDM.Contracts.BrokerCommodityDetails destination)
        {
            destination.Name = source.Name;
            destination.Broker = source.Broker.CreateNexusEntityId(() => source.Broker.LatestDetails.Name);
            destination.Commodity = source.Commodity.CreateNexusEntityId(() => source.Commodity.Name);
        }
    }
}