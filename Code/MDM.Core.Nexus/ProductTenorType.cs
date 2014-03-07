namespace EnergyTrading.MDM
{
    public partial class ProductTenorType
    {
        public virtual Product Product { get; set; }

        public virtual TenorType TenorType { get; set; }

        partial void CopyDetails(ProductTenorType details)
        {
            this.Product = details.Product;
            this.TenorType = details.TenorType;
        }
    }
}
