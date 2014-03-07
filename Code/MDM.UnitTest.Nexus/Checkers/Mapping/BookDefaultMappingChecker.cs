namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class BookDefaultMappingChecker : Checker<BookDefaultMapping>
    {
        public BookDefaultMappingChecker()
        {
            Compare(x => x.BookDefault).Id();
        }
    }
}
