namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class TenorChecker : Checker<Tenor>
    {
        public TenorChecker()
        {
            Compare(x => x.Id);

            Compare(x => x.Name);
            Compare(x => x.ShortName);
            Compare(x => x.TenorType);
            Compare(x => x.Delivery);
            Compare(x => x.DeliveryPeriod);
            Compare(x => x.Traded);

            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}
