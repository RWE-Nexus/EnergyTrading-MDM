namespace EnergyTrading.MDM.Test.Domain.EF
{
    using EnergyTrading.Mdm;

    public class CommodityMapping : EntityMapping
    {
        public virtual Commodity Commodity { get; set; }

        protected override IEntity Entity
        {
            get { return this.Commodity; }
            set { this.Commodity = value as Commodity; }
        }
    }
}