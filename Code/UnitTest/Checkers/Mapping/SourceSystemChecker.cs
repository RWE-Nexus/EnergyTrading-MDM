namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using EnergyTrading.Mdm;
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