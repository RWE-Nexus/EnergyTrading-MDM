namespace EnergyTrading.Mdm.Messages.Validators
{
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Validation;

    public class CreateMappingRequestValidator : Validator<CreateMappingRequest>
    {
        public CreateMappingRequestValidator(IValidatorEngine validatorEngine)
        {
            Rules.Add(new ChildRule<CreateMappingRequest, MdmId>(validatorEngine, p => p.Mapping));           
        }
    }
}