namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Test;

    public class HierarchyDetailsChecker : Checker<HierarchyDetails>
    {
        public HierarchyDetailsChecker()
        {
            this.Compare(x => x.Name);
        }
    }
}