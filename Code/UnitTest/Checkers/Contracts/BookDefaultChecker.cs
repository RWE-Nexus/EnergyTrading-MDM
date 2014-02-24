namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using EnergyTrading.Test;

    public class BookDefaultChecker : Checker<RWEST.Nexus.MDM.Contracts.BookDefault>
    {
        public BookDefaultChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.Nexus); 
            Compare(x => x.Audit);
            Compare(x => x.Links);
        }
    }
}
