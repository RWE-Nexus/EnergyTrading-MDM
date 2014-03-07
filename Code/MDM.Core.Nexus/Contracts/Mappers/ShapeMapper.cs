namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Shape" />
    /// </summary>
    public class ShapeMapper : ContractMapper<Shape, MDM.Shape, ShapeDetails, MDM.Shape, ShapeMapping>
    {
        public ShapeMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override ShapeDetails ContractDetails(Shape contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Shape contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(Shape contract)
        {
            return contract.Identifiers;
        }
    }
}