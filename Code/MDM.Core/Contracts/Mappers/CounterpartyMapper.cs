namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Data;

    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Counterparty" />
    /// </summary>
    public class CounterpartyMapper : ContractMapper<Counterparty, MDM.Counterparty, CounterpartyDetails, MDM.CounterpartyDetails, PartyRoleMapping>
    {
        private readonly IRepository repository;

        public CounterpartyMapper(IMappingEngine mappingEngine, IRepository repository) : base(mappingEngine)
        {
            this.repository = repository;
        }

        protected override CounterpartyDetails ContractDetails(Counterparty contract)
        {
            return contract.Details;
        }

        public override void Map(RWEST.Nexus.MDM.Contracts.Counterparty source, MDM.Counterparty destination)
        {
            base.Map(source, destination);
            destination.PartyRoleType = "Counterparty";
            destination.Party = this.repository.FindEntityByMapping<MDM.Party, PartyMapping>(source.Party);
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Counterparty contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(Counterparty contract)
        {
            return contract.Identifiers;
        }
    }
}