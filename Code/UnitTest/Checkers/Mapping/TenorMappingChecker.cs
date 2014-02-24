namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class TenorMappingChecker : Checker<TenorMapping>
    {
        public TenorMappingChecker()
        {
            Compare(x => x.Tenor).Id();
        }
    }
}
