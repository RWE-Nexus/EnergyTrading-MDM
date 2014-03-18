namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using EnergyTrading.Test;

    public class SystemDataChecker : Checker<EnergyTrading.Mdm.Contracts.SystemData>
    {
        public SystemDataChecker()
        {
            Compare(x => x.StartDate);
            Compare(x => x.EndDate);
        }
    }
}