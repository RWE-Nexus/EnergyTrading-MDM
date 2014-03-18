namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class ShapeDayDetailsMapper : Mapper<EnergyTrading.MDM.ShapeDay, OpenNexus.MDM.Contracts.ShapeDayDetails>
    {
        public override void Map(EnergyTrading.MDM.ShapeDay source, OpenNexus.MDM.Contracts.ShapeDayDetails destination)
        {
            destination.DayType = source.DayType;
            destination.Shape = source.Shape.CreateNexusEntityId(() => source.Shape.Name);
            destination.ShapeElement = source.ShapeElement.CreateNexusEntityId(() => source.ShapeElement.Name);
        }
    }
}