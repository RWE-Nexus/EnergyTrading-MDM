namespace EnergyTrading.MDM.Test.Domain.EF
{
    using EnergyTrading;
    using EnergyTrading.Mdm.Test;

    using NUnit.Framework;

    [TestFixture]
    public class CommodityMappingFixture : EntityMappingFixture<Commodity, CommodityMapping>
    {
        protected override void ProcessMapping(Commodity entity, CommodityMapping mapping)
        {
            entity.ProcessMapping(mapping);
        }

        protected override void AddMapping(Commodity entity, CommodityMapping mapping)
        {
            entity.Mappings.Add(mapping);
        }

        protected override void AssignEntity(CommodityMapping mapping, Commodity entity)
        {
            mapping.Commodity = entity;
        }

        protected override void AssignValidity(Commodity entity, DateRange range)
        {
            entity.Validity = range;
        }
    }
}