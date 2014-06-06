namespace EnergyTrading.Mdm.Contracts.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts;

    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="SourceSystem" />
    /// </summary>
    public class SourceSystemMapper : ContractMapper<SourceSystem, Mdm.SourceSystem, SourceSystemDetails, Mdm.SourceSystem, SourceSystemMapping>
    {
        public SourceSystemMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override SourceSystemDetails ContractDetails(SourceSystem contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(SourceSystem contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(SourceSystem contract)
        {
            return contract.Identifiers;
        }
    }
}