namespace EnergyTrading.Mdm.Contracts.Services
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides link enrichment for contracts
    /// </summary>
    /// <typeparam name="TContract"></typeparam>
    public interface IHyperlinkFactory<TContract>
    {
        void AddLinks(int mdmId, TContract contract);

        void AddLinks(int mdmId, IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> identifiers);
    }
}