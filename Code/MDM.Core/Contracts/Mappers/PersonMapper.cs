namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading;
    using EnergyTrading.Mapping;

    public class PersonMapper : ContractMapper<Person, MDM.Person, PersonDetails, MDM.PersonDetails, PersonMapping>
    {
        public PersonMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override PersonDetails ContractDetails(Person contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Person contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(Person contract)
        {
            return contract.Identifiers;
        }
    }
}