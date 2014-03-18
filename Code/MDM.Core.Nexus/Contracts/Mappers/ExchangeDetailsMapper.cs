namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Data;

    public class ExchangeDetailsMapper : Mapper<OpenNexus.MDM.Contracts.ExchangeDetails, MDM.ExchangeDetails>
    {
        private readonly IRepository repository;

        public ExchangeDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(OpenNexus.MDM.Contracts.ExchangeDetails source, MDM.ExchangeDetails destination)
        {
            destination.Name = source.Name;
            destination.Fax = source.Fax;
            destination.Phone = source.Phone;
        }
    }
}


