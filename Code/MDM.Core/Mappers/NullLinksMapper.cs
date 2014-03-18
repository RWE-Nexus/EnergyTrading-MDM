namespace EnergyTrading.MDM.Mappers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading.Contracts.Atom;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;

    public class NullLinksMapper : Mapper<IEntity, List<Link>>
    {
        public override void Map(IEntity source, List<Link> destination)
        {
            var links = new List<Link>();
        }
    }
}

