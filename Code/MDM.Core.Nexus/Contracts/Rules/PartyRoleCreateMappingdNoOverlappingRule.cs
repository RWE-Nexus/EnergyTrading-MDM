namespace EnergyTrading.MDM.Contracts.Rules
{
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Data;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.Validation;

    public class PartyRoleCreateMappingdNoOverlappingRule<TEntity, TMapping> : Rule<CreateMappingRequest>
        where TMapping : class, IIdentifiable, IEntityMapping
        where TEntity : class, IIdentifiable, IEntity
    {
        private const string MessageTemplate = "Identifier '{0}' for system '{1}' already assigned to an entity for some part of the range {2:yyyy-MMM-dd} to {3:yyyy-MMM-dd}";
        private const string EntityNotFoundMessageTemplate = "No {0} exists with identifier '{1}'";
        private readonly IRepository repository;

        public PartyRoleCreateMappingdNoOverlappingRule(IRepository repository)
        {
            this.repository = repository;
        }

        public override bool IsValid(CreateMappingRequest request)
        {
            var mapping = request.Mapping;
            var range = new EnergyTrading.DateRange(mapping.StartDate, mapping.EndDate);

            var entity = this.repository.FindOne<TEntity>(request.EntityId) as MDM.PartyRole;
            if(entity == null)
            {
                this.Message = string.Format(EntityNotFoundMessageTemplate, typeof(TEntity).Name, request.EntityId);
                return false;
            }

            var count = this.repository.FindPartyRoleOverlappingMappingCount<TMapping>(mapping.SystemName, mapping.Identifier, range, entity.PartyRoleType);
            if (count > 0)
            {
                this.Message = string.Format(MessageTemplate, mapping.Identifier, mapping.SystemName, range.Start, range.Finish);
                return false;
            }

            return true;
        }
    }
}