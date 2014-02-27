namespace EnergyTrading.MDM
{
    using System;

    public partial class Hierarchy
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

        partial void CopyDetails(Hierarchy details)
        {
            this.Name = details.Name;
        }
    }
}
