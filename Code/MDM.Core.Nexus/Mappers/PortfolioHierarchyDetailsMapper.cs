namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="PortfolioHierarchy" /> to a <see cref="RWEST.Nexus.MDM.Contracts.PortfolioHierarchyDetails" />
    /// </summary>
    public class PortfolioHierarchyDetailsMapper : Mapper<EnergyTrading.MDM.PortfolioHierarchy, OpenNexus.MDM.Contracts.PortfolioHierarchyDetails>
    {
        public override void Map(EnergyTrading.MDM.PortfolioHierarchy source, OpenNexus.MDM.Contracts.PortfolioHierarchyDetails destination)
        {
            destination.Hierarchy = source.Hierarachy.CreateNexusEntityId(() => source.Hierarachy.Name);
            destination.ChildPortfolio = source.ChildPortfolio.CreateNexusEntityId(() => source.ChildPortfolio.Name);
            destination.ParentPortfolio = source.ParentPortfolio.CreateNexusEntityId(() => source.ParentPortfolio.Name);
        }
    }
}