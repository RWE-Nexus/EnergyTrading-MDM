namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class TenorTypeChecker : Checker<TenorType>
    {
        public TenorTypeChecker()
        {
            Compare(x => x.Id);

            Compare(x => x.Name);
            Compare(x => x.ShortName);
            //Compare(x => x.Traded);

            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}
