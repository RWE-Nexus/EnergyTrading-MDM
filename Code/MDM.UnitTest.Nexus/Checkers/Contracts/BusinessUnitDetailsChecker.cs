namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using System;

    using EnergyTrading.Test;

    using BusinessUnitDetails = RWEST.Nexus.MDM.Contracts.BusinessUnitDetails;

    public class BusinessUnitDetailsChecker : Checker<BusinessUnitDetails>
    {
        public BusinessUnitDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.Phone);
            Compare(x => x.Fax);
            Compare(x => x.TaxLocation);
            Compare(x => x.AccountType);
            Compare(x => x.Address);
            Compare(x => x.Status);
        }
    }
}
