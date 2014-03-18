namespace EnergyTrading.MDM.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Contracts.Atom;
    using EnergyTrading.Mapping;

    using Party = EnergyTrading.MDM.Party;

    public class PartyLinksMapper : Mapper<EnergyTrading.MDM.Party, List<Link>>
    {
        public override void Map(EnergyTrading.MDM.Party source, List<Link> destination)
        {
            foreach (var partyRole in source.PartyRoles)
            {
                var entityIdentifier = partyRole.GetType().BaseType.Name;

                destination.Add(new Link() { Rel = "get-related-" + entityIdentifier.ToLower(), Type = entityIdentifier.ToString(), Uri = "/" + entityIdentifier + "/" + partyRole.Id });
            }
        }
    }
}
