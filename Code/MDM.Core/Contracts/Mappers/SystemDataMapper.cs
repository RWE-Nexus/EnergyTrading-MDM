namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading;
    using EnergyTrading.Mapping;

    public class SystemDataMapper : Mapper<RWEST.Nexus.MDM.Contracts.SystemData, DateRange>
    {
        public override DateRange Map(RWEST.Nexus.MDM.Contracts.SystemData source)
        {
            return new DateRange(source.StartDate, source.EndDate);
        }

        public override void Map(RWEST.Nexus.MDM.Contracts.SystemData source, DateRange destination)
        {
        }
    }
}