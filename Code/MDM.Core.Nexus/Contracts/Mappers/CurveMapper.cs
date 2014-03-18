namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Curve" />
    /// </summary>
    public class CurveMapper : ContractMapper<Curve, MDM.Curve, CurveDetails, MDM.Curve, CurveMapping>
    {
        public CurveMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override CurveDetails ContractDetails(Curve contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Curve contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(Curve contract)
        {
            return contract.Identifiers;
        }
    }
}