namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class UnitMappingChecker : Checker<UnitMapping>
    {
        public UnitMappingChecker()
        {
            Compare(x => x.Unit).Id();
        }
    }
}
