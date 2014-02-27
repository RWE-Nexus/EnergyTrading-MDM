namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class ShapeMappingChecker : Checker<ShapeMapping>
    {
        public ShapeMappingChecker()
        {
            Compare(x => x.Shape).Id();
        }
    }
}
