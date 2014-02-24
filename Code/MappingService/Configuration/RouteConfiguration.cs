namespace EnergyTrading.MDM.MappingService.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel.Activation;
    using System.Web;
    using System.Web.Routing;

    using EnergyTrading.Configuration;

    using AgreementService = EnergyTrading.MDM.MappingService.AgreementService;
    using BrokerRateService = EnergyTrading.MDM.MappingService.BrokerRateService;
    using BusinessUnitService = EnergyTrading.MDM.MappingService.BusinessUnitService;
    using CalendarService = EnergyTrading.MDM.MappingService.CalendarService;
    using CommodityFeeTypeService = EnergyTrading.MDM.MappingService.CommodityFeeTypeService;
    using CommodityInstrumentTypeService = EnergyTrading.MDM.MappingService.CommodityInstrumentTypeService;
    using CommodityService = EnergyTrading.MDM.MappingService.CommodityService;
    using FeeTypeService = EnergyTrading.MDM.MappingService.FeeTypeService;
    using InstrumentTypeService = EnergyTrading.MDM.MappingService.InstrumentTypeService;
    using LocationService = EnergyTrading.MDM.MappingService.LocationService;
    using MarketService = EnergyTrading.MDM.MappingService.MarketService;
    using PartyCommodityService = EnergyTrading.MDM.MappingService.PartyCommodityService;
    using PartyOverrideService = EnergyTrading.MDM.MappingService.PartyOverrideService;
    using PartyService = EnergyTrading.MDM.MappingService.PartyService;
    using PersonService = EnergyTrading.MDM.MappingService.PersonService;
    using PortfolioHierarchyService = EnergyTrading.MDM.MappingService.PortfolioHierarchyService;
    using PortfolioService = EnergyTrading.MDM.MappingService.PortfolioService;
    using ProductService = EnergyTrading.MDM.MappingService.ProductService;
    using ProductTypeInstanceService = EnergyTrading.MDM.MappingService.ProductTypeInstanceService;
    using ProductTypeService = EnergyTrading.MDM.MappingService.ProductTypeService;
    using ShipperCodeService = EnergyTrading.MDM.MappingService.ShipperCodeService;
    using SourceSystemService = EnergyTrading.MDM.MappingService.SourceSystemService;
    using VesselService = EnergyTrading.MDM.MappingService.VesselService;

    public class RouteConfiguration : IGlobalConfigurationTask
    {
        private readonly RouteCollection routes;

        public RouteConfiguration(RouteCollection routes)
        {
            this.routes = routes;
        }

        public IList<Type> DependsOn
        {
            get
            {
                return new List<Type> { };
            }
        }
       
        public void Configure()
        {
            var hostFactory = new WebServiceHostFactory();

            // TODO: Review before deployment. This allows for routes to not be added if the service is self hosted.
            if (HttpContext.Current == null)
            {
                return;
            }

            // TODO_RestServiceGeneration - add route for new entity
            this.routes.Add(new ServiceRoute("agreement", hostFactory, typeof(AgreementService)));
            this.routes.Add(new ServiceRoute("book", hostFactory, typeof(BookService)));
            this.routes.Add(new ServiceRoute("broker", hostFactory, typeof(BrokerService)));
            this.routes.Add(new ServiceRoute("brokercommodity", hostFactory, typeof(BrokerCommodityService)));
            this.routes.Add(new ServiceRoute("brokerrate", hostFactory, typeof(BrokerRateService)));
            this.routes.Add(new ServiceRoute("businessunit", hostFactory, typeof(BusinessUnitService)));
            this.routes.Add(new ServiceRoute("counterparty", hostFactory, typeof(CounterpartyService)));
            this.routes.Add(new ServiceRoute("curve", hostFactory, typeof(CurveService)));
            this.routes.Add(new ServiceRoute("calendar", hostFactory, typeof(CalendarService)));
            this.routes.Add(new ServiceRoute("commodity", hostFactory, typeof(CommodityService)));
            this.routes.Add(new ServiceRoute("commodityinstrumenttype", hostFactory, typeof(CommodityInstrumentTypeService)));
            this.routes.Add(new ServiceRoute("bookdefault", hostFactory, typeof(BookDefaultService)));
            this.routes.Add(new ServiceRoute("dimension", hostFactory, typeof(DimensionService)));
            this.routes.Add(new ServiceRoute("exchange", hostFactory, typeof(ExchangeService)));
            this.routes.Add(new ServiceRoute("hierarchy", hostFactory, typeof(HierarchyService)));
            this.routes.Add(new ServiceRoute("instrumenttype", hostFactory, typeof(InstrumentTypeService)));
            this.routes.Add(new ServiceRoute("instrumenttypeoverride", hostFactory, typeof(InstrumentTypeOverrideService)));
            this.routes.Add(new ServiceRoute("location", hostFactory, typeof(LocationService)));
            this.routes.Add(new ServiceRoute("market", hostFactory, typeof(MarketService)));
            this.routes.Add(new ServiceRoute("party", hostFactory, typeof(PartyService)));
            this.routes.Add(new ServiceRoute("partyaccountability", hostFactory, typeof(PartyAccountabilityService)));
            this.routes.Add(new ServiceRoute("partycommodity", hostFactory, typeof(PartyCommodityService)));
            this.routes.Add(new ServiceRoute("partyoverride", hostFactory, typeof(PartyOverrideService)));
            this.routes.Add(new ServiceRoute("partyrole", hostFactory, typeof(PartyRoleService)));
            this.routes.Add(new ServiceRoute("partyroleaccountability", hostFactory, typeof(PartyRoleAccountabilityService)));
            this.routes.Add(new ServiceRoute("person", hostFactory, typeof(PersonService)));
            this.routes.Add(new ServiceRoute("portfolio", hostFactory, typeof(PortfolioService)));
            this.routes.Add(new ServiceRoute("portfoliohierarchy", hostFactory, typeof(PortfolioHierarchyService)));
            this.routes.Add(new ServiceRoute("product", hostFactory, typeof(ProductService)));
            this.routes.Add(new ServiceRoute("productcurve", hostFactory, typeof(ProductCurveService)));
            this.routes.Add(new ServiceRoute("productscota", hostFactory, typeof(ProductScotaService)));
            this.routes.Add(new ServiceRoute("producttype", hostFactory, typeof(ProductTypeService)));
            this.routes.Add(new ServiceRoute("producttypeinstance", hostFactory, typeof(ProductTypeInstanceService)));
            this.routes.Add(new ServiceRoute("referencedata", hostFactory, typeof(ReferenceDataService)));
            this.routes.Add(new ServiceRoute("settlementcontact", hostFactory, typeof(SettlementContactService)));
            this.routes.Add(new ServiceRoute("shape", hostFactory, typeof(ShapeService)));
            this.routes.Add(new ServiceRoute("shapeday", hostFactory, typeof(ShapeDayService)));
            this.routes.Add(new ServiceRoute("shapeelement", hostFactory, typeof(ShapeElementService)));
            this.routes.Add(new ServiceRoute("shippercode", hostFactory, typeof(ShipperCodeService)));
            this.routes.Add(new ServiceRoute("sourcesystem", hostFactory, typeof(SourceSystemService)));
            this.routes.Add(new ServiceRoute("unit", hostFactory, typeof(UnitService)));
            this.routes.Add(new ServiceRoute("vessel", hostFactory, typeof(VesselService)));
            this.routes.Add(new ServiceRoute("FeeType", hostFactory, typeof(FeeTypeService)));
            this.routes.Add(new ServiceRoute("CommodityFeeType", hostFactory, typeof(CommodityFeeTypeService)));
            this.routes.Add(new ServiceRoute("tenor", hostFactory, typeof(TenorService)));
            this.routes.Add(new ServiceRoute("tenortype", hostFactory, typeof(TenorTypeService)));
            this.routes.Add(new ServiceRoute("producttenortype", hostFactory, typeof(ProductTenorTypeService)));
            this.routes.Add(new ServiceRoute("legalentity", hostFactory, typeof(LegalEntityService)));
        }
    }
}