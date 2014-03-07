namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    public class PartyRoleService : MdmService<RWEST.Nexus.MDM.Contracts.PartyRole, PartyRole, PartyRoleMapping, PartyRoleDetails, RWEST.Nexus.MDM.Contracts.PartyRoleDetails>
    {
        public PartyRoleService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) 
            : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<PartyRoleDetails> Details(PartyRole entity)
        {
            return entity.Details;
        }

        protected override IEnumerable<PartyRoleMapping> Mappings(PartyRole entity)
        {
            return entity.Mappings;
        }
    }
}

