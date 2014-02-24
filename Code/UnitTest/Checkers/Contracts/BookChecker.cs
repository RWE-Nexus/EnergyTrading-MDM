namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using EnergyTrading.Test;

    public class BookChecker : Checker<RWEST.Nexus.MDM.Contracts.Book>
    {
        public BookChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.Nexus); 
            Compare(x => x.Audit);
            Compare(x => x.Links);
        }
    }
}
