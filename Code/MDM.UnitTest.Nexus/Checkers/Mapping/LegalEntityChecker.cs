namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class LegalEntityChecker : Checker<LegalEntity>
    {
        public LegalEntityChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Details).Count();
            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}
