using EnergyTrading.MDM.Data;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Validation;
    using EnergyTrading.MDM.Contracts.Rules;

    public class CommodityValidator : Validator<Commodity>
    {
        public CommodityValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Commodity, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<Commodity>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.Commodity, MDM.Commodity, MDM.CommodityMapping>(repository, x => x.Details.Parent, false));
            Rules.Add(new ParentDiffersRule<OpenNexus.MDM.Contracts.Commodity, MDM.Commodity, MDM.CommodityMapping>(repository, x => x.Details.Name, x => x.Details.Parent, y => y.Name));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.Commodity, MDM.Unit, MDM.UnitMapping>(repository, x => x.Details.MassEnergyUnits, false));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.Commodity, MDM.Unit, MDM.UnitMapping>(repository, x => x.Details.VolumeEnergyUnits, false));
            Rules.Add(new NexusEntityExistsRule<OpenNexus.MDM.Contracts.Commodity, MDM.Unit, MDM.UnitMapping>(repository, x => x.Details.VolumetricDensityUnits, false));
        }
    }
}
		