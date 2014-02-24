namespace EnergyTrading.MDM.Contracts.Validators
{
    using RWEST.Nexus.MDM.Contracts;

    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.Data;
    using EnergyTrading.Validation;
    using RWEST.Nexus.MDM;

    public class NexusIdValidator<TMapping> : Validator<RWEST.Nexus.MDM.Contracts.NexusId>
        where TMapping : class, IEntityMapping
    {
        public NexusIdValidator(IRepository repository)
        {
            Rules.Add(new NexusIdNoOverlappingRule<TMapping>(repository));
            Rules.Add(new NexusIdSystemExistsRule(repository));
        }
    }
}