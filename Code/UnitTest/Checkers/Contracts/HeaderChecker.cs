namespace EnergyTrading.Mdm.Test.Checkers.Contracts
{
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Test;

    public class HeaderChecker : Checker<Header>
    {
        public HeaderChecker()
        {
            Compare(x => x.Notes);
            Compare(x => x.Version);
        }
    }
}