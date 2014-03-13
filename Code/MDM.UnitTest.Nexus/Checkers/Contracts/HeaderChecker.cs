namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using RWEST.Nexus.MDM.Contracts;
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