namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Calendar" />
    /// </summary>
    public class CalendarMapper : ContractMapper<Calendar, MDM.Calendar, CalendarDetails, MDM.Calendar, CalendarMapping>
    {
        public CalendarMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override CalendarDetails ContractDetails(Calendar contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Calendar contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(Calendar contract)
        {
            return contract.Identifiers;
        }
    }
}