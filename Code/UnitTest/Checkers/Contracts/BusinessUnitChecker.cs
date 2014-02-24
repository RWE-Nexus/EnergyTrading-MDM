namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using EnergyTrading.Test;

    public class BusinessUnitChecker : Checker<RWEST.Nexus.MDM.Contracts.BusinessUnit>
    {
        public BusinessUnitChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.Nexus); 
            Compare(x => x.Audit);
            Compare(x => x.Links);		
        }
    }
}
