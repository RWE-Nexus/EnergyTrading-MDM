namespace RWEST.Nexus.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using RWEST.Nexus.Test;

    public class CurveMappingChecker : Checker<CurveMapping>
    {
        public CurveMappingChecker()
        {
            Compare(x => x.Curve).Id();
        }
    }
}
