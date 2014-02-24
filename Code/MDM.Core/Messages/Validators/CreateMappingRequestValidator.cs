namespace EnergyTrading.MDM.Messages.Validators
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Validation;

    public class CreateMappingRequestValidator : Validator<CreateMappingRequest>
    {
        public CreateMappingRequestValidator(IValidatorEngine validatorEngine)
        {
            Rules.Add(new ChildRule<CreateMappingRequest, NexusId>(validatorEngine, p => p.Mapping));           
        }
    }
}