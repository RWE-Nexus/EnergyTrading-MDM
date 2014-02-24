namespace EnergyTrading.MDM.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RWEST.Nexus.MDM.Contracts;

    public static class EntityExtensions
    {
        public static EntityId CreateNexusEntityId(this IEntity entity, Func<string> name)
        {
            NexusId nexusMapping = CreateNexusMapping(entity);

            return nexusMapping == null ? null : new EntityId { Identifier = nexusMapping, Name = name() };
        }

        public static NexusId CreateNexusMapping(this IEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new NexusId
                {
                    SystemName = NexusName.Name, 
                    Identifier = entity.Id.ToString(), 
                    SourceSystemOriginated = false, 
                    IsNexusId = true, 
                    StartDate = entity.Validity.Start, 
                    EndDate = entity.Validity.Finish
                };
        }

        public static EnergyTrading.DateRange GetEntityValidity<T>(this IList<T> details) where T : IEntityDetail
        {
            if (details == null || details.Count == 0)
            {
                return EnergyTrading.DateRange.MaxDateRange;
            }

            var start = (from t in details select t.Validity.Start).Min();
            var finish = (from t in details select t.Validity.Finish).Max();
            return new EnergyTrading.DateRange(start, finish);
        }
    }
}