namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
    
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="BookDefault" />
    /// </summary>
    public class BookDefaultMapper : ContractMapper<BookDefault, MDM.BookDefault, BookDefaultDetails, MDM.BookDefault, BookDefaultMapping>
    {
        public BookDefaultMapper(IMappingEngine mappingEngine)
            : base(mappingEngine)
        {
        }

        protected override BookDefaultDetails ContractDetails(BookDefault contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(BookDefault contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(BookDefault contract)
        {
            return contract.Identifiers;
        }
    }
}