namespace EnergyTrading.MDM.Test
{
    using System;

    using EnergyTrading.Contracts.Search;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Search;

    public partial class ShipperCodeData
    {
        partial void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.ShipperCode contract)
        {
            var shipperCode = ObjectMother.Create<MDM.ShipperCode>();
            this.repository.Add(shipperCode);
            this.repository.Flush();

            contract.Details = new ShipperCodeDetails()
                {
                    Code = Guid.NewGuid().ToString(),
                    Location = shipperCode.Location.CreateNexusEntityId(() => shipperCode.Location.Name),
                    Party = shipperCode.Party.CreateNexusEntityId(() => shipperCode.Party.LatestDetails.Name)
                };

            // delete entity as we will attempt to post this exact contract and we may violate integrity constraints
            // the purpose of the persistence is to save the related object graph
            repository.Delete(shipperCode);
            repository.Flush();
        }

        partial void AddDetailsToEntity(MDM.ShipperCode entity, DateTime startDate, DateTime endDate)
        {
            var shipperCode = ObjectMother.Create<MDM.ShipperCode>();
            this.repository.Add(shipperCode);
            this.repository.Flush();

            entity.Party = shipperCode.Party;
            entity.Location = shipperCode.Location;
            entity.Code = Guid.NewGuid().ToString();
        }

        partial void CreateSearchData(Search search, MDM.ShipperCode entity1, MDM.ShipperCode entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Code", SearchCondition.Equals, entity1.Code)
                .AddCriteria("Code", SearchCondition.Equals, entity2.Code);
        }
    }
}
