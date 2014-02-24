namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using EnergyTrading.Test;

    public class CommodityFeeTypeChecker : Checker<RWEST.Nexus.MDM.Contracts.CommodityFeeType>
    {
        public CommodityFeeTypeChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.Nexus); 
            Compare(x => x.Audit);
            Compare(x => x.Links);		
        }
    }
}
