namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class CommodityChecker : Checker<Commodity>
    {
        public CommodityChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Name);
            Compare(x => x.Validity);
            Compare(x => x.Parent).Id();
            Compare(x => x.MassEnergyUnits).Id();
            Compare(x => x.VolumeEnergyUnits).Id();
            Compare(x => x.VolumetricDensityUnits).Id();
            Compare(x => x.MassEnergyValue);
            Compare(x => x.VolumeEnergyValue);
            Compare(x => x.VolumetricDensityValue);
            Compare(x => x.DeliveryRate);
            Compare(x => x.Mappings).Count();
        }
    }
}
