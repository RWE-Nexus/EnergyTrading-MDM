namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using EnergyTrading.Test;

    public class ReferenceDataChecker : Checker<ReferenceData>
    {
        public ReferenceDataChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Key);
            Compare(x => x.Value);
        }
    }
}