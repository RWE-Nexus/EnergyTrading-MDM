namespace EnergyTrading.Mdm.Extensions
{
    using System.Linq;
    using EnergyTrading.Mdm.Contracts;

    public static class MappingResponseExtensions
    {
        public static bool HasMultipleDefaultMapping(this MappingResponse response)
        {
            return response.Mappings.Where(x => x.DefaultReverseInd.HasValue && x.DefaultReverseInd.Value).Count() > 1;
        }

        public static bool HasMultipleMappingsWithNoDefault(this MappingResponse response)
        {
            return response.Mappings.Where(x => x.DefaultReverseInd.HasValue && x.DefaultReverseInd.Value).Count() == 0 && response.Mappings.Count > 1;
        }

        public static bool HasMutlipleMappingsWithOneDefault(this MappingResponse response)
        {
            return response.Mappings.Where(x => x.DefaultReverseInd.HasValue && x.DefaultReverseInd.Value).Count() == 1 && response.Mappings.Count > 1;
        }
    }
}