namespace EnergyTrading.MDM
{
    public partial class BookDefault
    {
        public string Name { get; set; }
        public virtual Person Trader { get; set; }
        public virtual PartyRole Desk { get; set; }
        public virtual string GfProductMapping { get; set; }
        public virtual Book Book { get; set; }
        public virtual string DefaultType { get; set; }
        public virtual PartyRole PartyRole { get; set; }

        partial void CopyDetails(BookDefault details)
        {
            this.Name = details.Name;
            this.Desk = details.Desk;
            this.Trader = details.Trader;
            this.GfProductMapping = details.GfProductMapping;
            this.Book = details.Book;
            this.DefaultType = details.DefaultType;
            this.PartyRole = details.PartyRole;
        }
    }
}
