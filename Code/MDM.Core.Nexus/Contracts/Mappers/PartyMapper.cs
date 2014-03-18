namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
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
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(Party contract)
        {
            return contract.Identifiers;
        }
    }
}