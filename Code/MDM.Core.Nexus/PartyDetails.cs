namespace EnergyTrading.MDM
{
    using System;

    using EnergyTrading;
    using EnergyTrading.Data;

    public class PartyDetails : IIdentifiable, IEntityDetail
    {
        public PartyDetails()
        {
            this.Validity = new DateRange();
            this.Timestamp = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        public int Id { get; set; }

        object IIdentifiable.Id
        {
            get { return this.Id; }
        }

        public virtual Party Party { get; set; }

        public IEntity Entity
        {
            get { return this.Party; }
            set { this.Party = value as Party; }
        }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Role { get; set; }

        public DateRange Validity { get; set; }

        public byte[] Timestamp { get; set; }

        public long Version
        {
            get { return BitConverter.ToInt64(this.Timestamp, 0); }
        }

        public bool IsInternal { get; set; }
    }
}
