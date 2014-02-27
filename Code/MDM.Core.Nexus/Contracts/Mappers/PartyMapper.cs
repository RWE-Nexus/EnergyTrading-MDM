namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading;
    using EnergyTrading.Mapping;

    public class PartyMapper : ContractMapper<Party, MDM.Party, PartyDetails, MDM.PartyDetails, PartyMapping>
    {
        public PartyMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override PartyDetails ContractDetails(Party contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Party contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(Party contract)
        {
            return contract.Identifiers;
        }
    }
}