namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Data;
    using EnergyTrading.Mapping;

    public class SourceSystemDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.SourceSystemDetails, MDM.SourceSystem>
    {
        private readonly IRepository repository;

        public SourceSystemDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(RWEST.Nexus.MDM.Contracts.SourceSystemDetails source, MDM.SourceSystem destination)
        {
            destination.Name = source.Name;
            destination.Parent = this.repository.FindEntityByMapping<MDM.SourceSystem, SourceSystemMapping>(source.Parent);
        }
    }
}