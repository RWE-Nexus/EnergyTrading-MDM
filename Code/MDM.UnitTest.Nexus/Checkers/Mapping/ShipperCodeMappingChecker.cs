namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class ShipperCodeMappingChecker : Checker<ShipperCodeMapping>
    {
        public ShipperCodeMappingChecker()
        {
            Compare(x => x.ShipperCode).Id();
        }
    }
}
