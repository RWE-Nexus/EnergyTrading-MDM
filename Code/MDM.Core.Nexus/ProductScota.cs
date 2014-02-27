namespace EnergyTrading.MDM
{
    public partial class ProductScota
    {
        public string Name { get; set; }

        public virtual Product Product { get; set; }

        public virtual Location ScotaDeliveryPoint { get; set; }

        public virtual Location ScotaOrigin { get; set; }

        public string ScotaContract { get; set; }

        public string ScotaRss { get; set; }

        public string ScotaVersion { get; set; }

        partial void CopyDetails(ProductScota details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfProduct = this.Product;
            var forceLoadScotaDeliveryPoint = this.ScotaDeliveryPoint;
            var forceLoadScoataOrigin = this.ScotaOrigin;

            this.Name = details.Name;
            this.Product = details.Product;
            this.ScotaDeliveryPoint = details.ScotaDeliveryPoint;
            this.ScotaOrigin = details.ScotaOrigin;
            this.ScotaContract = details.ScotaContract;
            this.ScotaRss = details.ScotaRss;
            this.ScotaVersion = details.ScotaVersion;
        }
    }
}
