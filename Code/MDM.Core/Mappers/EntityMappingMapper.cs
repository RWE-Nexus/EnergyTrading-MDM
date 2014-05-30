namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts;

    public class EntityMappingMapper : Mapper<IEntityMapping, MdmId>
    {
        public override void Map(IEntityMapping source, MdmId destination)
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