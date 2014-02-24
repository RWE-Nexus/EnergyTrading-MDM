namespace EnergyTrading.MDM
{
    using System;

    public partial class Portfolio
    {
        private string portfolioType;

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public string PortfolioType
        {
            get
            {
                return this.portfolioType;
            }
            set
            {
                this.portfolioType = value;
            }
        }

        public virtual PartyRole BusinessUnit { get; set; }

        partial void CopyDetails(Portfolio details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfBusinessUnit = this.BusinessUnit;

            this.Name = details.Name;
            this.PortfolioType = details.PortfolioType;
            this.BusinessUnit = details.BusinessUnit;
        }

        
    }
}