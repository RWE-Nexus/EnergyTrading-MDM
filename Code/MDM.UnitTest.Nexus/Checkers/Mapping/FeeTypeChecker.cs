namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class FeeTypeChecker : Checker<FeeType>
    {
        public FeeTypeChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Name);
            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}
