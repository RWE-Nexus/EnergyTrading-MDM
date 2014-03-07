namespace EnergyTrading.MDM
{
    public partial class PortfolioHierarchy
    {
        public virtual Portfolio ChildPortfolio { get; set; }

        public virtual Hierarchy Hierarachy { get; set; }

        public virtual Portfolio ParentPortfolio { get; set; }

        partial void CopyDetails(PortfolioHierarchy details)
        {
            var forceLoadOfParentPoftfolio = this.ParentPortfolio;
            var forceLoadOfChildPortfolio = this.ChildPortfolio;
            var forceLoadOfHierarchy = this.Hierarachy;

            this.ParentPortfolio = details.ParentPortfolio;
            this.ChildPortfolio = details.ChildPortfolio;
            this.Hierarachy = details.Hierarachy;
        }
    }
}