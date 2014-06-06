namespace EnergyTrading.Mdm.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Data;

    public class SourceSystemDetailsMapper : Mapper<EnergyTrading.Mdm.Contracts.SourceSystemDetails, Mdm.SourceSystem>
    {
        private readonly IRepository repository;

        public SourceSystemDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(EnergyTrading.Mdm.Contracts.SourceSystemDetails source, Mdm.SourceSystem destination)
        {
            destination.Name = source.Name;
            destination.Parent = this.repository.FindEntityByMapping<Mdm.SourceSystem, SourceSystemMapping>(source.Parent);
        }
    }
}