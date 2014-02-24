namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;
    using EnergyTrading.Mapping;
    using RWEST.Nexus.MDM.Contracts;
    
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Agreement" />
    /// </summary>
    public class AgreementMapper : ContractMapper<Agreement, MDM.Agreement, AgreementDetails, MDM.Agreement, AgreementMapping>
    {
        public AgreementMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override AgreementDetails ContractDetails(Agreement contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Agreement contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(Agreement contract)
        {
            return contract.Identifiers;
        }
    }
}