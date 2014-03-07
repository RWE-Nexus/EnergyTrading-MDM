namespace EnergyTrading.MDM
{
    public partial class Book
    {
        private string name;

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

        partial void CopyDetails(Book details)
        {
            this.Name = details.Name;
        }
    }
}
