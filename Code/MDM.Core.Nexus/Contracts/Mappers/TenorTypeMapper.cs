namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="TenorType" />
    /// </summary>
    public class TenorTypeMapper : ContractMapper<TenorType, MDM.TenorType, TenorTypeDetails, MDM.TenorType, TenorTypeMapping>
    {
        public TenorTypeMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override TenorTypeDetails ContractDetails(TenorType contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(TenorType contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(TenorType contract)
        {
            return contract.Identifiers;
        }
    }
}