namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="Broker" /> to a <see cref="RWEST.Nexus.MDM.Contracts.BrokerDetails" />
    /// </summary>
    public class BrokerDetailsMapper : Mapper<EnergyTrading.MDM.BrokerDetails, RWEST.Nexus.MDM.Contracts.BrokerDetails>
    {
        public override void Map(EnergyTrading.MDM.BrokerDetails source, RWEST.Nexus.MDM.Contracts.BrokerDetails destination)
        {
            destination.Name = source.Name;
            destination.Fax = source.Fax;
            destination.Rate = source.Rate;
            destination.Phone = source.Phone;
        }
    }
}