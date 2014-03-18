namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="BookDefault" /> to a <see cref="RWEST.Nexus.MDM.Contracts.BookDefaultDetails" />
    /// </summary>
    public class BookDefaultDetailsMapper : Mapper<EnergyTrading.MDM.BookDefault, OpenNexus.MDM.Contracts.BookDefaultDetails>
    {
        public override void Map(EnergyTrading.MDM.BookDefault source, OpenNexus.MDM.Contracts.BookDefaultDetails destination)
        {
            destination.Name = source.Name;
            destination.Trader = source.Trader.CreateNexusEntityId(() => source.Trader.LatestDetails.FirstName + " " + source.Trader.LatestDetails.LastName);
            destination.Desk = source.Desk.CreateNexusEntityId(() => source.Desk.LatestDetails.Name);
            destination.Book = source.Book.CreateNexusEntityId(() => source.Book.Name);
            destination.GfProductMapping = source.GfProductMapping;
            destination.DefaultType = source.DefaultType;
            destination.PartyRole = source.PartyRole.CreateNexusEntityId(() => source.PartyRole.LatestDetails.Name);
        }
    }
}