namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class CommodityInstrumentTypeMappingChecker : Checker<CommodityInstrumentTypeMapping>
    {
        public CommodityInstrumentTypeMappingChecker()
        {
            Compare(x => x.CommodityInstrumentType).Id();
        }
    }
}
