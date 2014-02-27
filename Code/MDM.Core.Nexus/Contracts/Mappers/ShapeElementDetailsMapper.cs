namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Mapping;

    public class ShapeElementDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.ShapeElementDetails, MDM.ShapeElement>
    {
        public override void Map(RWEST.Nexus.MDM.Contracts.ShapeElementDetails source, MDM.ShapeElement destination)
        {
            destination.Name = source.Name;
            destination.Period = new EnergyTrading.DateRange(source.Period.StartDate, source.Period.EndDate);
        }
    }
}