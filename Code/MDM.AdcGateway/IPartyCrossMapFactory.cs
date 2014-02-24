using System.Collections.Generic;
using System.Linq;
using MDM.Sync.Synchronizers.Adc;

namespace MDM.AdcGateway
{
    internal interface IPartyCrossMapFactory
    {
        List<MappingCounterparty> CreateMappingCounterparties(IQueryable data);
        void PartySource();
    }
}