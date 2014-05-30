namespace EnergyTrading.MDM.Contracts.Rules
{
    using System;

    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Data;
    using EnergyTrading.Validation;

    public class ParentDiffersRule<TEntity, TRelatedEntity, TMapping> : Rule<TEntity>
        where TRelatedEntity : class, IEntity
        where TEntity : class, IMdmEntity
        where TMapping : class, IEntityMapping
        //where TDetails : class
   {
        private const string ParentNotFoundMessageTemplate = "Parent with id '{0}' not found";
        private const string MessageTemplate = "Parent must not be same as entity";
        private readonly IRepository repository;
        private readonly Func<TEntity, string> accessorEntityName;
        private readonly Func<TEntity, EntityId> accessorParentId;
        private readonly Func<TRelatedEntity, string> accessorParentName;

        public ParentDiffersRule(IRepository repository, Func<TEntity, string> accessorEntityName, Func<TEntity, EntityId> accessorParentId, Func<TRelatedEntity, string> accessorParentName)
        {
            this.repository = repository;
            this.accessorEntityName = accessorEntityName;
            this.accessorParentId = accessorParentId;
            this.accessorParentName = accessorParentName;
  }

        public override bool IsValid(TEntity entity)
        {
            var parentId = this.accessorParentId.Invoke(entity);

            if (parentId == null)
            {
                return true;
            }

            var parent = repository.FindEntityByMapping<TRelatedEntity, TMapping>(parentId);

            if (parent == null)
            {
                Message = string.Format(ParentNotFoundMessageTemplate, parentId.Identifier.Identifier);
                return false;
            }

            var entityName = this.accessorEntityName.Invoke(entity);
            var parentName = this.accessorParentName.Invoke(parent);

            if (entityName == parentName)
            {
                Message = string.Format(MessageTemplate);
                return false;
            }

            return true;
        }
    }
}
