namespace EnergyTrading.MDM.Messages.Validators
{
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.Validation;

    public class MappingRequestValidator : Validator<MappingRequest>
    {
        public MappingRequestValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new SystemExistsRule(repository));           
        }
    }
}