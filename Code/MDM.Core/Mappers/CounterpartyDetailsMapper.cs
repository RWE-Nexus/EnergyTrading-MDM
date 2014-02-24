namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="Counterparty" /> to a <see cref="RWEST.Nexus.MDM.Contracts.CounterpartyDetails" />
    /// </summary>
    public class CounterpartyDetailsMapper : Mapper<EnergyTrading.MDM.CounterpartyDetails, RWEST.Nexus.MDM.Contracts.CounterpartyDetails>
    {
        public override void Map(EnergyTrading.MDM.CounterpartyDetails source, RWEST.Nexus.MDM.Contracts.CounterpartyDetails destination)
        {
            destination.Name = source.Name;
            destination.Fax = source.Fax;
            destination.Phone = source.Phone;
            destination.ShortName = source.ShortName;
        }
    }
}
