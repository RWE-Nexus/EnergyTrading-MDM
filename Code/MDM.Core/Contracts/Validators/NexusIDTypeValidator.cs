namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Mdm.Contracts;

    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.Data;
    using EnergyTrading.Validation;
    using EnergyTrading.Mdm;

    public class NexusIdValidator<TMapping> : Validator<EnergyTrading.Mdm.Contracts.MdmId>
        where TMapping : class, IEntityMapping
    {
        public NexusIdValidator(IRepository repository)
        {
            Rules.Add(new NexusIdNoOverlappingRule<TMapping>(repository));
            Rules.Add(new MdmIdSystemExistsRule(repository));
        }
    }
}