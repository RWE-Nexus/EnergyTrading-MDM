namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.Validation;

    public class PartyRoleNexusIdValidator<TMapping> : Validator<EnergyTrading.Mdm.Contracts.MdmId>
        where TMapping : class, IEntityMapping
    {
        public PartyRoleNexusIdValidator(IRepository repository)
        {
            //this.Rules.Add(new PartyRoleNexusIdNoOverlappingRule<TMapping>(repository));
            this.Rules.Add(new MdmIdSystemExistsRule(repository));
        }
    }
}