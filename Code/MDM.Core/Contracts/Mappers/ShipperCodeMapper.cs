namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="ShipperCode" />
    /// </summary>
    public class ShipperCodeMapper : ContractMapper<ShipperCode, MDM.ShipperCode, ShipperCodeDetails, MDM.ShipperCode, ShipperCodeMapping>
    {
        public ShipperCodeMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override ShipperCodeDetails ContractDetails(ShipperCode contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(ShipperCode contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(ShipperCode contract)
        {
            return contract.Identifiers;
        }
    }
}