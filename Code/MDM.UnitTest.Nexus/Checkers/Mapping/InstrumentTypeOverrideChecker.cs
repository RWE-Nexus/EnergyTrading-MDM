namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class InstrumentTypeOverrideChecker : Checker<InstrumentTypeOverride>
    {
        public InstrumentTypeOverrideChecker()
        {
            Compare(x => x.Id);

            Compare(x => x.Name);
            Compare(x => x.ProductType);
            Compare(x => x.Broker);
            Compare(x => x.CommodityInstrumentType);
            Compare(x => x.InstrumentSubType);
            Compare(x => x.ProductTenorType);

            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}
