namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class SettlementContactMapper : ContractMapper<SettlementContact, MDM.SettlementContact, SettlementContactDetails, MDM.SettlementContact, PartyAccountabilityMapping>
    {
        public SettlementContactMapper(IMappingEngine mappingEngine)
            : base(mappingEngine)
        {
        }

        protected override SettlementContactDetails ContractDetails(SettlementContact contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(SettlementContact contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(SettlementContact contract)
        {
            return contract.Identifiers;
        }
    }
}