namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using System;

    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Test;

    public class ProductScotaDetailsChecker : Checker<ProductScotaDetails>
    {
        public ProductScotaDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.Product);
            Compare(x => x.ScotaDeliveryPoint);
            Compare(x => x.ScotaOrigin);
            Compare(x => x.ScotaRss);
            Compare(x => x.ScotaContract);
            Compare(x => x.ScotaVersion);
        }
    }
}
