namespace EnergyTrading.MDM.Test
{
    using System;

    using EnergyTrading.Contracts.Search;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Search;

    using DateRange = EnergyTrading.DateRange;
    using Person = EnergyTrading.MDM.Person;
    using PersonDetails = EnergyTrading.MDM.PersonDetails;

    public partial class PersonData
    {
        partial void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.Person contract)
        {
            contract.Details = new RWEST.Nexus.MDM.Contracts.PersonDetails { Forename = Guid.NewGuid().ToString(), Surname = Guid.NewGuid().ToString(), Email = "test@test.com" };
        }

        partial void AddDetailsToEntity(Person entity, DateTime startDate, DateTime endDate)
        {
            entity.AddDetails(
                new PersonDetails
                    { FirstName = Guid.NewGuid().ToString(), Email = "test@test.com", Validity = new DateRange(startDate, endDate) });
        }

        partial void CreateSearchData(Search search, Person entity1, Person entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Firstname", SearchCondition.Equals, entity1.LatestDetails.FirstName)
                .AddCriteria("Firstname", SearchCondition.Equals, entity2.LatestDetails.FirstName);
        }
    }
}