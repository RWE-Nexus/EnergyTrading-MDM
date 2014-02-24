namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
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
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(TenorType contract)
        {
            return contract.Identifiers;
        }
    }
}