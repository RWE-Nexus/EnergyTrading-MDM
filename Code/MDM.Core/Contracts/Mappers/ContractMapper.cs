namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading;
    using EnergyTrading.Mapping;

    /// <summary>
    /// Maps an MDM contract to an entity using a <see cref="IMappingEngine" /> for the internal elements.
    /// </summary>
    /// <typeparam name="TContract"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TContractDetails"></typeparam>
    /// <typeparam name="TEntityDetails"></typeparam>
    /// <typeparam name="TMapping"></typeparam>
    public abstract class ContractMapper<TContract, TEntity, TContractDetails, TEntityDetails, TMapping> : Mapper<TContract, TEntity>
        where TEntity : class, IEntity, new()
        where TMapping : IEntityMapping
        where TEntityDetails : IEntityDetail
    {
        private readonly IMappingEngine mappingEngine;

        protected ContractMapper(IMappingEngine mappingEngine)
        {
            this.mappingEngine = mappingEngine;
        }

        public override void Map(TContract source, TEntity destination)
        {
            // Map all the identifiers
            foreach (var id in this.Identifiers(source))
            {
                var mapping = this.mappingEngine.Map<EnergyTrading.Mdm.Contracts.MdmId, TMapping>(id);
                destination.ProcessMapping(mapping);
            }

            // And the details
            var details = this.mappingEngine.Map<TContractDetails, TEntityDetails>(this.ContractDetails(source));
            details.Validity = this.ContractDetailsValidity(source);
            destination.AddDetails(details);
        }

        protected abstract TContractDetails ContractDetails(TContract contract);

        protected abstract DateRange ContractDetailsValidity(TContract contract);

        protected abstract IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(TContract contract);

        protected DateRange SystemDataValidity(EnergyTrading.Mdm.Contracts.SystemData system)
        {
            return system == null
                       ? DateRange.MaxDateRange
                       : new DateRange(system.StartDate, system.EndDate);
        }
    }
}