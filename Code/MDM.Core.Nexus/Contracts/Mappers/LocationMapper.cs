namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Location" />
    /// </summary>
    public class LocationMapper : ContractMapper<Location, MDM.Location, LocationDetails, MDM.Location, LocationMapping>
    {
        public LocationMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override LocationDetails ContractDetails(Location contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Location contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(Location contract)
        {
            return contract.Identifiers;
        }
    }
}