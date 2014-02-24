namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using EnergyTrading.Test;

    public class InstrumentTypeOverrideChecker : Checker<RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride>
    {
        public InstrumentTypeOverrideChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.Nexus); 
            Compare(x => x.Audit);
            Compare(x => x.Links);		
        }
    }
}
