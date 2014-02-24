namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class ShapeElementMappingChecker : Checker<ShapeElementMapping>
    {
        public ShapeElementMappingChecker()
        {
            Compare(x => x.ShapeElement).Id();
        }
    }
}
