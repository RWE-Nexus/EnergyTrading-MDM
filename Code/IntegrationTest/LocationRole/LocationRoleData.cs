namespace EnergyTrading.MDM.Test
{
    using System;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Search;

    using DateRange = EnergyTrading.DateRange;

    public partial class LocationRoleData
    {
        partial void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.LocationRole contract)
        {
            var locationRole = ObjectMother.Create<LocationRole>();
            this.repository.Add(locationRole);
            this.repository.Flush();

            contract.Details = new RWEST.Nexus.MDM.Contracts.LocationRoleDetails()
                {
                   Location = locationRole.Location.CreateNexusEntityId(() => locationRole.Location.Name),
                    Type = locationRole.Type.Name
                };
        }

        partial void AddDetailsToEntity(MDM.LocationRole entity, DateTime startDate, DateTime endDate)
        {
            var locationRole = ObjectMother.Create<LocationRole>();
            this.repository.Add(locationRole);
            this.repository.Flush();

            var locationRoleDetails =
            new LocationRole()
                {
                    Location = locationRole.Location,
                    Type = locationRole.Type
                };

            locationRoleDetails.Validity = new DateRange(startDate, endDate);
            entity.AddDetails(locationRoleDetails);
        }

        partial void  CreateSearchData(Search search, LocationRole entity1, LocationRole entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
