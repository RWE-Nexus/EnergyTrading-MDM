namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Data;

    /// <summary>
	///
	/// </summary>
    public class BrokerDetailsMapper : Mapper<OpenNexus.MDM.Contracts.BrokerDetails, MDM.BrokerDetails>
    {
        private IRepository repository;

        public BrokerDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(OpenNexus.MDM.Contracts.BrokerDetails source, MDM.BrokerDetails destination)
        {
            destination.Phone = source.Phone;
            destination.Rate = source.Rate;
            destination.Fax = source.Fax;
            destination.Name = source.Name;
        }
    }
}