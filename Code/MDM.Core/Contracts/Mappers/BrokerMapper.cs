namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Data;

    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Broker" />
    /// </summary>
    public class BrokerMapper : ContractMapper<Broker, MDM.Broker, BrokerDetails, MDM.BrokerDetails, PartyRoleMapping>
    {
        private readonly IRepository repository;

        public BrokerMapper(IMappingEngine mappingEngine, IRepository repository) : base(mappingEngine)
        {
            this.repository = repository;
        }

        protected override BrokerDetails ContractDetails(Broker contract)
        {
            return contract.Details;
        }

        public override void Map(RWEST.Nexus.MDM.Contracts.Broker source, MDM.Broker destination)
        {
            base.Map(source, destination);
            destination.PartyRoleType = "Broker";
            destination.Party = this.repository.FindEntityByMapping<MDM.Party, PartyMapping>(source.Party);
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Broker contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(Broker contract)
        {
            return contract.Identifiers;
        }
    }
}