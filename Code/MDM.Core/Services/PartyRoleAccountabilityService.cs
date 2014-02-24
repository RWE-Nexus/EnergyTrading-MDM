namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    public class PartyRoleAccountabilityService : MdmService<RWEST.Nexus.MDM.Contracts.PartyRoleAccountability, PartyRoleAccountability, PartyRoleAccountabilityMapping, PartyRoleAccountability, RWEST.Nexus.MDM.Contracts.PartyRoleAccountabilityDetails>
    {
        public PartyRoleAccountabilityService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache)
            : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<PartyRoleAccountability> Details(PartyRoleAccountability entity)
        {
            return new List<PartyRoleAccountability> { entity };
        }

        protected override IEnumerable<PartyRoleAccountabilityMapping> Mappings(PartyRoleAccountability entity)
        {
            return entity.Mappings;
        }
    }
}