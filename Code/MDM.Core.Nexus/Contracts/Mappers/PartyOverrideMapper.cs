namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="PartyOverride" />
    /// </summary>
    public class PartyOverrideMapper : ContractMapper<PartyOverride, MDM.PartyOverride, PartyOverrideDetails, MDM.PartyOverride, PartyOverrideMapping>
    {
        public PartyOverrideMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override PartyOverrideDetails ContractDetails(PartyOverride contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(PartyOverride contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(PartyOverride contract)
        {
            return contract.Identifiers;
        }
    }
}