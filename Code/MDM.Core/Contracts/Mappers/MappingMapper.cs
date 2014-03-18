namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.MDM.Data;
    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;

    public class MappingMapper<TMapping> : Mapper<EnergyTrading.Mdm.Contracts.MdmId, TMapping>
        where TMapping : class, IEntityMapping, new()
    {
        private readonly IRepository repository;

        public MappingMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(EnergyTrading.Mdm.Contracts.MdmId source, TMapping destination)
        {
            destination.MappingId = (int) (source.MappingId.HasValue ? source.MappingId.Value : 0L);
            destination.System = this.repository.SystemByName(source.SystemName);
            destination.MappingValue = source.Identifier;
            destination.IsMaster = source.SourceSystemOriginated;
            destination.IsDefault = source.DefaultReverseInd.HasValue && source.DefaultReverseInd.Value;
            destination.Validity = new DateRange(source.StartDate, source.EndDate);
        }
    }
}