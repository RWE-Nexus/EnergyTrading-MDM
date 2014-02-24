namespace EnergyTrading.MDM.Contracts.Services
{
    using EnergyTrading.Contracts.Atom;

    using RWEST.Nexus.MDM.Contracts;

    public class HyperlinkFactory<TContract> : IHyperlinkFactory<TContract>
        where TContract : IMdmEntity
    {
        private readonly string baseRelUri;
        private readonly string baseUri;

        public HyperlinkFactory(string baseUri, string baseRelUri)
        {
            this.baseUri = baseUri;
            this.baseRelUri = baseRelUri;
        }

        public void AddLinks(int mdmId, TContract contract)
        {
            contract.Links.Add(new RWEST.Nexus.Contracts.Atom.Link { Uri = this.baseUri + "/" + mdmId, Rel = "self" });
            contract.Links.Add(new RWEST.Nexus.Contracts.Atom.Link { Uri = this.baseUri + "/" + mdmId + "/mappings", Rel = this.baseRelUri + "/mappings" });

            this.AddLinks(mdmId, contract.Identifiers);
        }

        public void AddLinks(int mdmId, System.Collections.Generic.IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> identifiers)
        {
            foreach (var id in identifiers)
            {
                var link = new RWEST.Nexus.Contracts.Atom.Link { Uri = this.baseUri + "/" + mdmId + "/mapping/" + id.MappingId, Rel = "self" };
            }
        }
    }
}