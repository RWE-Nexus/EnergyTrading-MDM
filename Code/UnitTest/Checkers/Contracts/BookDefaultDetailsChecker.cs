namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Test;

    public class BookDefaultDetailsChecker : Checker<BookDefaultDetails>
    {
        public BookDefaultDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.GfProductMapping);
            Compare(x => x.Book);
            Compare(x => x.Trader);
            Compare(x => x.Desk);
            Compare(x => x.DefaultType);
            Compare(x => x.PartyRole);
        }
    }
}
