namespace EnergyTrading.Mdm.Contracts.Rules
{
    using System;

    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Mdm.Data;
    using EnergyTrading.Validation;

    public class NexusEntityExistsRule<TEntity, TRelatedEntity, TMapping> : Rule<TEntity>
        where TEntity : class 
        where TMapping : class, IEntityMapping
        where TRelatedEntity : class, IEntity 

    {
        private const string EntityExistsMessageTemplate = "{0} does not have a valid {1} with id '{2}'";
        private const string NullMessageTemplate = "{0} requires a valid {1}";

        private readonly IRepository repository;
        private readonly Func<TEntity, EntityId> accessor;
        private readonly bool isRequired;

        public NexusEntityExistsRule(IRepository repository, Func<TEntity, EntityId> accessor, bool isRequired)
        {
            this.repository = repository;
            this.accessor = accessor;
            this.isRequired = isRequired;
        }

        public override bool IsValid(TEntity entity)
        {
            var entityId = this.accessor.Invoke(entity);

            if (!this.isRequired && entityId == null)
            {
                return true;
            }

            if (this.isRequired && entityId == null)
            {
                Message = string.Format(NullMessageTemplate, typeof(TEntity).Name, typeof(TRelatedEntity).Name);
                return false;
            }

            var entityDoesNotExist = this.repository.FindEntityByMapping<TRelatedEntity, TMapping>(entityId) == null;

            if (entityDoesNotExist)
            {
                Message = string.Format(EntityExistsMessageTemplate, typeof(TEntity).Name, typeof(TRelatedEntity).Name, entityId.Identifier.Identifier);
                return false;
            }

            return true;
        }
    }
}

