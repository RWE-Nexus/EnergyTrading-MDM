namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class AgreementChecker : Checker<Agreement>
    {
        public AgreementChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Name);
            Compare(x => x.PaymentTerms);
            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}
