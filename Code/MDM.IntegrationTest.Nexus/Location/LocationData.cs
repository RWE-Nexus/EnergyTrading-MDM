namespace EnergyTrading.MDM.Test
{
    using System;

    using EnergyTrading.Contracts.Search;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Search;

    using DateRange = EnergyTrading.DateRange;
    using Location = EnergyTrading.MDM.Location;

    public partial class LocationData
    {
        partial void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.Location contract)
        {
            var locationType = Guid.NewGuid().ToString();
            this.repository.Add<MDM.ReferenceData>(
                new MDM.ReferenceData() { Key = "LocationType", Value = locationType });
            this.repository.Flush();

            contract.Details = new LocationDetails { Name = Guid.NewGuid().ToString(), Type = locationType };
        }

        partial void AddDetailsToEntity(Location entity, DateTime startDate, DateTime endDate)
        {
            var locationType = "LocationType" + new Guid();
            this.repository.Add(
                new MDM.ReferenceData() { Key = "LocationType", Value = "LoctionType" + Guid.NewGuid() });
            this.repository.Flush();

            var locationDetails = new Location { Name = Guid.NewGuid().ToString(), Type = locationType };

            locationDetails.Validity = new DateRange(startDate, endDate);
            entity.AddDetails(locationDetails);
        }

        partial void  CreateSearchData(Search search, Location entity1, Location entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}