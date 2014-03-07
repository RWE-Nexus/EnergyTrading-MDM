namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class AgreementMappingChecker : Checker<AgreementMapping>
    {
        public AgreementMappingChecker()
        {
            Compare(x => x.Agreement).Id();
        }
    }
}
