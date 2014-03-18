namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
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
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(SettlementContact contract)
        {
            return contract.Identifiers;
        }
    }
}