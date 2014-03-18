namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Dimension" />
    /// </summary>
    public class DimensionMapper : ContractMapper<Dimension, MDM.Dimension, DimensionDetails, MDM.Dimension, DimensionMapping>
    {
        public DimensionMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override DimensionDetails ContractDetails(Dimension contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Dimension contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(Dimension contract)
        {
            return contract.Identifiers;
        }
    }
}