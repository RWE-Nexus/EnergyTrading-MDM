namespace EnergyTrading.MDM.Contracts.Rules
{
    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Data;
    using EnergyTrading.Validation;

    public class MdmIdSystemExistsRule : Rule<EnergyTrading.Mdm.Contracts.MdmId>
    {
        private const string MessageTemplate = "No system named '{0}' was found";
        private readonly IRepository repository;

        public MdmIdSystemExistsRule(IRepository repository)
        {
            this.repository = repository;
        }

        public override bool IsValid(MdmId entity)
        {
            if (entity.SystemName == MdmInternalName.Name)
            {
                return true;
            }

            var system = this.repository.SystemByName(entity.SystemName);
            if (system == null)
            {
                Message = string.Format(MessageTemplate, entity.SystemName);
                return false;
            }

            return true;
        }
    }
}
