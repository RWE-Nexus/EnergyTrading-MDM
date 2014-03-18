namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;
    using EnergyTrading.Mapping;
    using OpenNexus.MDM.Contracts;
    
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
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(Agreement contract)
        {
            return contract.Identifiers;
        }
    }
}