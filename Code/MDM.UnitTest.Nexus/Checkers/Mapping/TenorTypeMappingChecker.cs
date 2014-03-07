namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class TenorTypeMappingChecker : Checker<TenorTypeMapping>
    {
        public TenorTypeMappingChecker()
        {
            Compare(x => x.TenorType).Id();
        }
    }
}
