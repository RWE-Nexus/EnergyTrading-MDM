namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class SourceSystemChecker : Checker<SourceSystem>
    {
        public SourceSystemChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Parent).Id();
            Compare(x => x.Name);
        }
    }
}