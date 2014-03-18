namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Tenor" />
    /// </summary>
    public class TenorMapper : ContractMapper<Tenor, MDM.Tenor, TenorDetails, MDM.Tenor, TenorMapping>
    {
        public TenorMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override TenorDetails ContractDetails(Tenor contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Tenor contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(Tenor contract)
        {
            return contract.Identifiers;
        }
    }
}