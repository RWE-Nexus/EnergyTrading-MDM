namespace EnergyTrading.MDM.Contracts.Rules
{
    using System;

    using EnergyTrading.Data;
    using EnergyTrading.MDM.Data;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.Validation;

    public class AmendMappingNoOverlappingRule<T> : Rule<AmendMappingRequest> where T : class, IIdentifiable, IEntityMapping
    {
        private const string MessageTemplate = "Identifier '{0}' for system '{1}' already assigned to an entity for some part of the range {2:yyyy-MMM-dd} to {3:yyyy-MMM-dd}";
        private readonly IRepository repository;

        public AmendMappingNoOverlappingRule(IRepository repository)
        {
            this.repository = repository;
        }

        public override bool IsValid(AmendMappingRequest amendMappingRequest)
        {
            var mapping = amendMappingRequest.Mapping;
            var range = new EnergyTrading.DateRange(mapping.StartDate, mapping.EndDate);
            var count = this.repository.FindOverlappingMappingCount<T>(mapping.SystemName, mapping.Identifier, range, amendMappingRequest.MappingId);
            if (count > 0)
            {
                this.Message = string.Format(MessageTemplate, mapping.Identifier, mapping.SystemName, range.Start, range.Finish);
                return false;
            }

            return true;
        }
    }
}