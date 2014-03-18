namespace EnergyTrading.MDM
{
    using System;

    public partial class Unit
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Symbol { get; set; }

        public decimal ConversionFactor { get; set; }

        public decimal ConversionConstant { get; set; }

        public virtual Dimension Dimension { get; set; }

        partial void CopyDetails(Unit details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfDimension = details.Dimension;

            this.Name = details.Name;
            this.Description = details.Description;
            this.Symbol = details.Symbol;
            this.ConversionFactor = details.ConversionFactor;
            this.ConversionConstant = details.ConversionConstant;
            this.Dimension = forceLoadOfDimension;
        }
    }
}
