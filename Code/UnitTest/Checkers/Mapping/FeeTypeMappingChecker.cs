namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class FeeTypeMappingChecker : Checker<FeeTypeMapping>
    {
        public FeeTypeMappingChecker()
        {
            Compare(x => x.FeeType).Id();
        }
    }
}
