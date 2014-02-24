namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
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
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(Curve contract)
        {
            return contract.Identifiers;
        }
    }
}