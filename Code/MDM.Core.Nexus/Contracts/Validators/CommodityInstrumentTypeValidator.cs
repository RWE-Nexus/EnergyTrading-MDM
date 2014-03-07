using EnergyTrading.MDM.Contracts.Rules;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class CommodityInstrumentTypeValidator : Validator<CommodityInstrumentType>
    {
        public CommodityInstrumentTypeValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<CommodityInstrumentType, NexusId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new NexusEntityExistsRule<RWEST.Nexus.MDM.Contracts.CommodityInstrumentType, MDM.Commodity, MDM.CommodityMapping>(repository, x => x.Details.Commodity, true));
            Rules.Add(new NexusEntityExistsRule<RWEST.Nexus.MDM.Contracts.CommodityInstrumentType, MDM.InstrumentType, MDM.InstrumentTypeMapping>(repository, x => x.Details.InstrumentType, false));
        }
    }
}