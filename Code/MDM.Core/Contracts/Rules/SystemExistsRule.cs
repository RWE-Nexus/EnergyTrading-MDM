namespace EnergyTrading.Mdm.Contracts.Rules
{
    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Data;
    using EnergyTrading.Mdm.Messages;
    using EnergyTrading.Validation;

    public class SystemExistsRule : Rule<MappingRequest>
    {
        private const string MessageTemplate = "No system named '{0}' was found";
        private readonly IRepository repository;

        public SystemExistsRule(IRepository repository)
        {
            this.repository = repository;
        }

        public override bool IsValid(MappingRequest entity)
        {
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