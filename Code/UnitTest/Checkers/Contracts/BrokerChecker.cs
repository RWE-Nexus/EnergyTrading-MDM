namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using EnergyTrading.Test;

    public class BrokerChecker : Checker<RWEST.Nexus.MDM.Contracts.Broker>
    {
        public BrokerChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.Nexus); 
            Compare(x => x.Audit);
            Compare(x => x.Links);		
        }
    }
}
