namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
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
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(BookDefault contract)
        {
            return contract.Identifiers;
        }
    }
}