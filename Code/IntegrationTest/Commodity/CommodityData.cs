namespace EnergyTrading.MDM.Test
{
    using System;
    using Extensions;
    using EnergyTrading.Contracts.Search;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Search;

    using Commodity = EnergyTrading.MDM.Commodity;
    using Unit = EnergyTrading.MDM.Unit;

    public partial class CommodityData
    {
        partial void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.Commodity contract)
        {
            var commodity = ObjectMother.Create<Commodity>();
            repository.Add(commodity);
            repository.Flush();

            contract.Details = new CommodityDetails()
                {
                    Name = Guid.NewGuid().ToString(),
                    Parent = commodity.CreateNexusEntityId(() => ""),
                    MassEnergyUnits = commodity.MassEnergyUnits.CreateNexusEntityId(() => ""),
                    VolumeEnergyUnits = commodity.VolumeEnergyUnits.CreateNexusEntityId(() => ""),
                    VolumetricDensityUnits = commodity.VolumetricDensityUnits.CreateNexusEntityId(() => ""),
                    MassEnergyValue = 1350,
                    VolumeEnergyValue = 34.45m,
                    VolumetricDensityValue = 324,
                    DeliveryRate = "Month"
                };
        }

        partial void AddDetailsToEntity(MDM.Commodity entity, DateTime startDate, DateTime endDate)
        {
            entity.Name = Guid.NewGuid().ToString();
            //entity.Parent = new Commodity() { Name = Guid.NewGuid().ToString() };
            entity.MassEnergyUnits = new Unit() { Name = Guid.NewGuid().ToString(), Symbol = Guid.NewGuid().ToString() };
            entity.VolumeEnergyUnits = new Unit() { Name = Guid.NewGuid().ToString(), Symbol = Guid.NewGuid().ToString() };
            entity.VolumetricDensityUnits = new Unit() { Name = Guid.NewGuid().ToString(), Symbol = Guid.NewGuid().ToString() };
            entity.MassEnergyValue = 234;
            entity.VolumeEnergyValue = 23.45m;
            entity.VolumetricDensityValue = 345;
        }

        partial void  CreateSearchData(Search search, Commodity entity1, Commodity entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}