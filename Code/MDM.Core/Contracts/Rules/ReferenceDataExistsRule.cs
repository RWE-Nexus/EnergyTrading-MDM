using System;
using EnergyTrading.Data;
using EnergyTrading.Mdm.Data;
using EnergyTrading.Validation;

namespace EnergyTrading.Mdm.Contracts.Rules
{
    public class ReferenceDataExistsRule<TEntity> : Rule<TEntity>
        where TEntity : class
    {
        private const string EntityExistsMessageTemplate = "{0} does not have a valid {1} with id '{2}'";
        private const string NullMessageTemplate = "{0} requires a valid {1}";

        private readonly IRepository repository;
        private readonly Func<TEntity, string> accessor;
        private readonly string referenceDataType;
        private readonly bool isRequired;

        public ReferenceDataExistsRule(IRepository repository, Func<TEntity, string> accessor, string referenceDataType, bool isRequired = false)
        {
            this.repository = repository;
            this.accessor = accessor;
            this.referenceDataType = referenceDataType;
            this.isRequired = isRequired;
        }

        public override bool IsValid(TEntity entity)
        {
            var value = accessor.Invoke(entity);

            if (!isRequired && value == null)
            {
                return true;
            }

            if (isRequired && value == null)
            {
                Message = string.Format(NullMessageTemplate, typeof(TEntity).Name, referenceDataType);
                return false;
            }

            if (!repository.ReferenceDataExists(referenceDataType, value))
            {
                Message = string.Format(EntityExistsMessageTemplate, typeof(TEntity).Name, referenceDataType, value);
                return false;
            }

            return true;
        }
    }
}