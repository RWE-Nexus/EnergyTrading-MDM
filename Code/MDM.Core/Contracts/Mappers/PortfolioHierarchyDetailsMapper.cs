namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Data;

    public class PortfolioHierarchyDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.PortfolioHierarchyDetails, MDM.PortfolioHierarchy>
    {
        private readonly IRepository repository;

        public PortfolioHierarchyDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(RWEST.Nexus.MDM.Contracts.PortfolioHierarchyDetails source, MDM.PortfolioHierarchy destination)
        {
            destination.Hierarachy = this.repository.FindEntityByMapping<MDM.Hierarchy, HierarchyMapping>(source.Hierarchy);
            destination.ChildPortfolio = this.repository.FindEntityByMapping<MDM.Portfolio, PortfolioMapping>(source.ChildPortfolio);
            destination.ParentPortfolio = this.repository.FindEntityByMapping<MDM.Portfolio, PortfolioMapping>(source.ParentPortfolio);
        }
    }
}