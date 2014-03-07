namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class BookDefaultChecker : Checker<BookDefault>
    {
        public BookDefaultChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Name);
            Compare(x => x.GfProductMapping);
            Compare(x => x.Book);
            Compare(x => x.Trader);
            Compare(x => x.Desk);
            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
            Compare(x => x.DefaultType);
            Compare(x => x.PartyRole);
        }
    }
}
