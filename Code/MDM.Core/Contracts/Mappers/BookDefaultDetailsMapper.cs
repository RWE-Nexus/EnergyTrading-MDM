namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Data;

    public class BookDefaultDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.BookDefaultDetails, MDM.BookDefault>
    {
        private readonly IRepository repository;

        public BookDefaultDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(RWEST.Nexus.MDM.Contracts.BookDefaultDetails source, MDM.BookDefault destination)
        {
            destination.Book = this.repository.FindEntityByMapping<MDM.Book, BookMapping>(source.Book);
            if (source.Desk != null)
            {
                destination.Desk = this.repository.FindEntityByMapping<MDM.PartyRole, PartyRoleMapping>(source.Desk);
            }

            if (source.Trader != null)
            {
                destination.Trader = repository.FindEntityByMapping<MDM.Person, PersonMapping>(source.Trader);
            }
            destination.Name = source.Name;
            destination.GfProductMapping = source.GfProductMapping;
            destination.DefaultType = source.DefaultType;
            if (source.PartyRole != null)
            {
                destination.PartyRole = repository.FindEntityByMapping<PartyRole, PartyRoleMapping>(source.PartyRole);
            }
        }
    }
}