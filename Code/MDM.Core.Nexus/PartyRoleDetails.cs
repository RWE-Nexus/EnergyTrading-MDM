namespace EnergyTrading.MDM
{
    using System;

    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Extensions;

    public class PartyRoleDetails : IIdentifiable, IEntityDetail
    {
        public PartyRoleDetails()
        {
            this.Validity = new DateRange();
            this.Timestamp = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        public int Id { get; set; }

        object IIdentifiable.Id
        {
            get { return this.Id; }
        }

        public virtual string Name { get; set; }

        public virtual PartyRole PartyRole { get; set; }

        public IEntity Entity
        {
            get { return this.PartyRole; }
            set { this.PartyRole = value as PartyRole; }
        }

        public DateRange Validity { get; set; }

        public byte[] Timestamp { get; set; }

        public ulong Version
        {
            get { return this.Timestamp.ToUnsignedLongVersion(); }
        }
    }
}