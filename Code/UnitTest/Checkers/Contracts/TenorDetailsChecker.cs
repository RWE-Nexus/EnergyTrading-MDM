namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Test;

    public class TenorDetailsChecker : Checker<TenorDetails>
    {
        public TenorDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.ShortName);
            Compare(x => x.TenorType);
            Compare(x => x.IsRelative);
            Compare(x => x.DeliveryRangeType);
            Compare(x => x.DeliveryPeriod);
            Compare(x => x.Delivery);
            Compare(x => x.Traded);
        }
    }
}
