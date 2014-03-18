namespace EnergyTrading.MDM
{
    public partial class Agreement
    {
        public string Name { get; set; }

        public string PaymentTerms { get; set; }

        partial void CopyDetails(Agreement details)
        {
             this.Name = details.Name;
            this.PaymentTerms = details.PaymentTerms;
        }
    }
}
