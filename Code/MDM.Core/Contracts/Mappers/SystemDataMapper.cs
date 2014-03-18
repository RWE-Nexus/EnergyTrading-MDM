namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading;
    using EnergyTrading.Mapping;

    public class SystemDataMapper : Mapper<EnergyTrading.Mdm.Contracts.SystemData, DateRange>
    {
        public override DateRange Map(EnergyTrading.Mdm.Contracts.SystemData source)
        {
            return new DateRange(source.StartDate, source.EndDate);
        }

        public override void Map(EnergyTrading.Mdm.Contracts.SystemData source, DateRange destination)
        {
        }
    }
}