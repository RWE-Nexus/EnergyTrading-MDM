namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Unit" />
    /// </summary>
    public class UnitMapper : ContractMapper<Unit, MDM.Unit, UnitDetails, MDM.Unit, UnitMapping>
    {
        public UnitMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override UnitDetails ContractDetails(Unit contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Unit contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(Unit contract)
        {
            return contract.Identifiers;
        }
    }
}