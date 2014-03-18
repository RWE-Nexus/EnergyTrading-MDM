namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Vessel" />
    /// </summary>
    public class VesselMapper : ContractMapper<Vessel, MDM.Vessel, VesselDetails, MDM.Vessel, VesselMapping>
    {
        public VesselMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override VesselDetails ContractDetails(Vessel contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Vessel contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(Vessel contract)
        {
            return contract.Identifiers;
        }
    }
}