namespace EnergyTrading.MDM.Contracts.Rules
{
    using RWEST.Nexus.MDM.Contracts;

    using EnergyTrading.MDM.Data;
    using EnergyTrading.Data;
    using EnergyTrading.Validation;

    public class NexusIdSystemExistsRule : Rule<RWEST.Nexus.MDM.Contracts.NexusId>
    {
        private const string MessageTemplate = "No system named '{0}' was found";
        private readonly IRepository repository;

        public NexusIdSystemExistsRule(IRepository repository)
        {
            this.repository = repository;
        }

        public override bool IsValid(NexusId entity)
        {
            if (entity.SystemName == NexusName.Name)
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
