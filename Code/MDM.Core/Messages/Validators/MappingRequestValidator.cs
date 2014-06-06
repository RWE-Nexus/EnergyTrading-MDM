namespace EnergyTrading.Mdm.Messages.Validators
{
    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Contracts.Rules;
    using EnergyTrading.Validation;

    public class MappingRequestValidator : Validator<MappingRequest>
    {
        public MappingRequestValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new SystemExistsRule(repository));           
        }
    }
}