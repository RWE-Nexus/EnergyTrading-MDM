namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

            public class AgreementService : MdmService<OpenNexus.MDM.Contracts.Agreement, Agreement, AgreementMapping, Agreement, OpenNexus.MDM.Contracts.AgreementDetails>
        {

    public AgreementService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<Agreement> Details(Agreement entity)
        {
            return new List<Agreement> { entity };
            }

        protected override IEnumerable<AgreementMapping> Mappings(Agreement entity)
        {
            return entity.Mappings;
        }
    }
}