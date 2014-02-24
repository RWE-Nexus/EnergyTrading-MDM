using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDM.Sync.Synchronizers.Adc;

namespace MDM.AdcGateway
{
    public class PartyCrossMapFactory : IPartyCrossMapFactory
    {
        public List<MappingCounterparty> CreateMappingCounterparties(IQueryable data)
        {
           var mappingCounterparties = new List<MappingCounterparty>();

            foreach(var item in data)
            {
                var partyCrossMap = item as PartyCrossMap;

                var sourceSystem = new Mapping
                                 {
                                     Id = partyCrossMap.MapId1,
                                     System = partyCrossMap.System1,
                                     Value = partyCrossMap.MapValue1
                                 };

                var targetSystem = new Mapping
                                {
                                    Id = partyCrossMap.MapId2,
                                    System = partyCrossMap.System2,
                                    Value = partyCrossMap.MapValue2
                                };

                mappingCounterparties.Add(CreateMappingCounterparty(sourceSystem, targetSystem, partyCrossMap.Commodity));

            }

            return mappingCounterparties;
        }

        private MappingCounterparty CreateMappingCounterparty(Mapping sourceSystem, Mapping targetSystem, string commodity)
        {
            return new MappingCounterparty
                       {
                           SourceSystem = sourceSystem,
                           TargetSystem = targetSystem,
                           Commodity = commodity
                       };
        }

        public void PartySource()
        {
            throw new NotImplementedException();
        }
    }
}
