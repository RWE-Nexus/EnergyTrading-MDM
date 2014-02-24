namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class DimensionChecker : Checker<Dimension>
    {
        public DimensionChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Name);
            Compare(x => x.Description);
            Compare(x => x.MassDimension);
            Compare(x => x.LengthDimension);
            Compare(x => x.LuminousIntensityDimension);
            Compare(x => x.TimeDimension);
            Compare(x => x.TemperatureDimension);
            Compare(x => x.ElectricCurrentDimension);
            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}
