namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class ShapeDayDetailsMapper : Mapper<EnergyTrading.MDM.ShapeDay, RWEST.Nexus.MDM.Contracts.ShapeDayDetails>
    {
        public override void Map(EnergyTrading.MDM.ShapeDay source, RWEST.Nexus.MDM.Contracts.ShapeDayDetails destination)
        {
            destination.DayType = source.DayType;
            destination.Shape = source.Shape.CreateNexusEntityId(() => source.Shape.Name);
            destination.ShapeElement = source.ShapeElement.CreateNexusEntityId(() => source.ShapeElement.Name);
        }
    }
}