namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
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
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(Calendar contract)
        {
            return contract.Identifiers;
        }
    }
}