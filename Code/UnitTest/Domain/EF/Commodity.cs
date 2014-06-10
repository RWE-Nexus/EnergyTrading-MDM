namespace EnergyTrading.MDM.Test.Domain.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading.Data;
    using EnergyTrading.Mdm;
    using EnergyTrading.Mdm.Extensions;

    public class Commodity : IIdentifiable, IEntity, IEntityDetail
    {
        public Commodity()
        {
            this.Mappings = new List<CommodityMapping>();
            this.Validity = new DateRange();
            this.Timestamp = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        public int Id { get; set; }

        object IIdentifiable.Id
        {
            get { return this.Id; }
        }

        IEntity IRangedChild.Entity
        {
            get { return this; }
            set { }
        }

        public virtual string Name { get; set; }

        public virtual IList<CommodityMapping> Mappings { get; private set; }

        IList<IEntityMapping> IEntity.Mappings
        {
            get { return this.Mappings.ToList<IEntityMapping>(); }
        }

        public DateRange Validity { get; set; }

        /// <summary>
        /// Gets or sets the Timestamp property.
        /// <para>
        /// This is implemented by a RowVersion property on the database.
        /// </para>
        /// </summary>
        public byte[] Timestamp { get; set; }

        public ulong Version
        {
            get { return this.Timestamp.ToUnsignedLongVersion(); }
        }

        public void AddDetails(Commodity details)
        {
            // Sanity checks
            if (details == null)
            {
                throw new ArgumentNullException("details");
            }

            // Copy the bits across
            CopyDetails(details);
            this.Validity = details.Validity;

            // Trim all the mappings that extend past the end of the entity.
            this.Mappings.TrimMappings(this.Validity.Finish);
        }

        public void ProcessMapping(CommodityMapping mapping)
        {
            this.ProcessMapping(this.Mappings, mapping, this.Validity.Finish);      
        }

        void IEntity.AddDetails(IEntityDetail details)
        {
            this.AddDetails(details as Commodity);
        }

        void IEntity.ProcessMapping(IEntityMapping mapping)
        {
            this.ProcessMapping(mapping as CommodityMapping);
        }

        private void CopyDetails(Commodity details)
        {            
        }
    }
}
