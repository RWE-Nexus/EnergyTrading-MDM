namespace EnergyTrading.Mdm.Contracts.Rules
{
    using System;

    using EnergyTrading.Validation;

    public class PredicateRule<TEntity> : Rule<TEntity>
    {
        private Predicate<TEntity> check;
        private string messageOnInvalid;

        public PredicateRule(Predicate<TEntity> check, string messageOnInvalid)
        {
            this.check = check;
            this.messageOnInvalid = messageOnInvalid;
        }

        public override bool IsValid(TEntity entity)
         {
            var valid = check(entity);
            if (!valid)
            {
                this.Message = messageOnInvalid;
            }

            return valid;
        }
    }
}