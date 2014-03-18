namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Data;
    using EnergyTrading.Mapping;

    public class ShapeDayDetailsMapper : Mapper<OpenNexus.MDM.Contracts.ShapeDayDetails, MDM.ShapeDay>
    {
        private IRepository repository;

        public ShapeDayDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(OpenNexus.MDM.Contracts.ShapeDayDetails source, MDM.ShapeDay destination)
        {
            destination.DayType = source.DayType;
            destination.Shape = repository.FindEntityByMapping<MDM.Shape, ShapeMapping>(source.Shape);
            destination.ShapeElement = repository.FindEntityByMapping<MDM.ShapeElement, ShapeElementMapping>(source.ShapeElement);
        }
    }
}