namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="ShapeElement" />
    /// </summary>
    public class ShapeElementMapper : ContractMapper<ShapeElement, MDM.ShapeElement, ShapeElementDetails, MDM.ShapeElement, ShapeElementMapping>
    {
        public ShapeElementMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override ShapeElementDetails ContractDetails(ShapeElement contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(ShapeElement contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(ShapeElement contract)
        {
            return contract.Identifiers;
        }
    }
}