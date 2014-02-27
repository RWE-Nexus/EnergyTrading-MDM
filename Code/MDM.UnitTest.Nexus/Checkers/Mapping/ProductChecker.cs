namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class ProductChecker : Checker<Product>
    {
        public ProductChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Name);
            Compare(x => x.Market).Id();
            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
            Compare(x => x.LotSize);
            Compare(x => x.Shape).Id();
            Compare(x => x.DefaultCurve).Id();
            Compare(x => x.CommodityInstrumentType).Id();
            Compare(x => x.InstrumentSubType);
        }
    }
}
