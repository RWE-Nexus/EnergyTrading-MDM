namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using System;

    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Test;

    public class AgreementDetailsChecker : Checker<AgreementDetails>
    {
        public AgreementDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.PaymentTerms);
        }
    }
}
