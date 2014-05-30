namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.Validation;

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