namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class ShapeDayMappingChecker : Checker<ShapeDayMapping>
    {
        public ShapeDayMappingChecker()
        {
            Compare(x => x.ShapeDay).Id();
        }
    }
}
