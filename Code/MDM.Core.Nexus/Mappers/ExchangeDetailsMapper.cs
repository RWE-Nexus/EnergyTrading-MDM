namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class ExchangeDetailsMapper : Mapper<EnergyTrading.MDM.ExchangeDetails, OpenNexus.MDM.Contracts.ExchangeDetails>
    {
        public override void Map(EnergyTrading.MDM.ExchangeDetails source, OpenNexus.MDM.Contracts.ExchangeDetails destination)
        {
            destination.Name = source.Name;
            destination.Fax = source.Fax;
            destination.Phone = source.Phone;
        }
    }
}

