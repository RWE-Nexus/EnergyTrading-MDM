namespace EnergyTrading.MDM.Contracts.Rules
{
    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Data;
    using EnergyTrading.Validation;

    public class NexusIdNoOverlappingRule<T> : Rule<EnergyTrading.Mdm.Contracts.MdmId>
        where T : class, IIdentifiable, IEntityMapping
    {
        private const string MessageTemplate = "Identifier '{0}' for system '{1}' already assigned to an entity for some part of the range {2:yyyy-MMM-dd} to {3:yyyy-MMM-dd}";
        private readonly IRepository repository;

        public NexusIdNoOverlappingRule(IRepository repository)
        {
            this.repository = repository;
        }

        public override bool IsValid(MdmId entity)
        {
            var range = new EnergyTrading.DateRange(entity.StartDate, entity.EndDate);
            var count = this.repository.FindOverlappingMappingCount<T>(entity.SystemName, entity.Identifier, range);
            if (count > 0)
            {
                Message = string.Format(MessageTemplate, entity.Identifier, entity.SystemName, range.Start, range.Finish);
                return false;
            }

            return true;
        }
    }
}