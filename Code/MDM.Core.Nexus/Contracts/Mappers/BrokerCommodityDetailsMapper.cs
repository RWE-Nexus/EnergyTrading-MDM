using EnergyTrading.Data;
using EnergyTrading.MDM.Data;

namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Mapping;

    /// <summary>
	///
	/// </summary>
    public class BrokerCommodityDetailsMapper : Mapper<OpenNexus.MDM.Contracts.BrokerCommodityDetails, MDM.BrokerCommodity>
    {
        private readonly IRepository repository;

        public BrokerCommodityDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(OpenNexus.MDM.Contracts.BrokerCommodityDetails source, MDM.BrokerCommodity destination)
        {
            destination.Name = source.Name;
            destination.Broker = this.repository.FindEntityByMapping<MDM.Broker, PartyRoleMapping>(source.Broker);
            destination.Commodity = this.repository.FindEntityByMapping<MDM.Commodity, LocationMapping>(source.Commodity);
            
        }
    }
}