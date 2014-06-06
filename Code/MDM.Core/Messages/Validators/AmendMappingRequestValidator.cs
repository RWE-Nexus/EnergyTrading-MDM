﻿namespace EnergyTrading.Mdm.Messages.Validators
{
    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Mdm.Contracts.Rules;
    using EnergyTrading.Validation;

    public class AmendMappingRequestValidator<TMapping> : Validator<AmendMappingRequest>
        where TMapping : class, IEntityMapping
    {
        public AmendMappingRequestValidator(IRepository repository)
        {
            Rules.Add(new AmendMappingNoOverlappingRule<TMapping>(repository));
        }
    }
}