namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class ProductScotaChecker : Checker<ProductScota>
    {
        public ProductScotaChecker()
        {
            Compare(x => x.Id);

            Compare(x => x.Name);
            Compare(x => x.Product);
            Compare(x => x.ScotaDeliveryPoint);
            Compare(x => x.ScotaOrigin);
            Compare(x => x.ScotaRss);
            Compare(x => x.ScotaContract);
            Compare(x => x.ScotaVersion);

            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}
