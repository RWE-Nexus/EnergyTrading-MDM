namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class ExchangeDetailsMapper : Mapper<EnergyTrading.MDM.ExchangeDetails, RWEST.Nexus.MDM.Contracts.ExchangeDetails>
    {
        public override void Map(EnergyTrading.MDM.ExchangeDetails source, RWEST.Nexus.MDM.Contracts.ExchangeDetails destination)
        {
            destination.Name = source.Name;
            destination.Fax = source.Fax;
            destination.Phone = source.Phone;
        }
    }
}

