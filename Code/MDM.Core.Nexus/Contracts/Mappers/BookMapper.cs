namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Mapping;
    using RWEST.Nexus.MDM.Contracts;

    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Book" />
    /// </summary>
    public class BookMapper : ContractMapper<Book, MDM.Book, BookDetails, MDM.Book, BookMapping>
    {
        public BookMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override BookDetails ContractDetails(Book contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Book contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(Book contract)
        {
            return contract.Identifiers;
        }
    }
}