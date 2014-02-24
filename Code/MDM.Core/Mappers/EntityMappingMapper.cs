namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
    using RWEST.Nexus.MDM;

    public class EntityMappingMapper : Mapper<IEntityMapping, NexusId>
    {
        public override void Map(IEntityMapping source, NexusId destination)
        {
            destination.SystemName = source.System.Name;
            destination.Identifier = source.MappingValue;
            destination.MappingId = (int)source.Id;
            destination.SourceSystemOriginated = source.IsMaster;
            destination.DefaultReverseInd = source.IsDefault ? (bool?)true : null;
            destination.StartDate = source.Validity.Start;
            destination.EndDate = source.Validity.Finish;
        }
    }
}