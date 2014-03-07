namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class UnitChecker : Checker<Unit>
    {
        public UnitChecker()
        {
            Compare(x => x.Id);

            Compare(x => x.Description);
            Compare(x => x.Dimension).Id();
            Compare(x => x.ConversionConstant);
            Compare(x => x.ConversionFactor);
            Compare(x => x.Symbol);

            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}
