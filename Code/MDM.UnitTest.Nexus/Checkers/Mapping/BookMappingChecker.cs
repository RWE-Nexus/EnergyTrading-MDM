namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class BookMappingChecker : Checker<BookMapping>
    {
        public BookMappingChecker()
        {
            Compare(x => x.Book).Id();
        }
    }
}
