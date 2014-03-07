namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;
    using RWEST.Nexus.MDM.Contracts;

    public class DimensionDetailsChecker : Checker<DimensionDetails>
    {
        public DimensionDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.Description);
            Compare(x => x.MassDimension);
            Compare(x => x.LengthDimension);
            Compare(x => x.LuminousIntensityDimension);
            Compare(x => x.TimeDimension);
            Compare(x => x.TemperatureDimension);
            Compare(x => x.ElectricCurrentDimension);
        }
    }
}
