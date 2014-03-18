namespace EnergyTrading.MDM.ServiceHost.Wcf.Nexus.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel.Activation;
    using System.Web;
    using System.Web.Routing;

    using EnergyTrading.Configuration;

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
            this.routes.Add(new ServiceRoute("agreement", hostFactory, typeof(Legacy.AgreementService)));
            this.routes.Add(new ServiceRoute("book", hostFactory, typeof(Legacy.BookService)));
            this.routes.Add(new ServiceRoute("broker", hostFactory, typeof(Legacy.BrokerService)));
            this.routes.Add(new ServiceRoute("brokercommodity", hostFactory, typeof(Legacy.BrokerCommodityService)));
            this.routes.Add(new ServiceRoute("brokerrate", hostFactory, typeof(Legacy.BrokerRateService)));
            this.routes.Add(new ServiceRoute("calendar", hostFactory, typeof(Legacy.CalendarService)));
            this.routes.Add(new ServiceRoute("businessunit", hostFactory, typeof(Legacy.BusinessUnitService)));
            this.routes.Add(new ServiceRoute("counterparty", hostFactory, typeof(Legacy.CounterpartyService)));
            this.routes.Add(new ServiceRoute("curve", hostFactory, typeof(Legacy.CurveService)));
            this.routes.Add(new ServiceRoute("commodity", hostFactory, typeof(Legacy.CommodityService)));
            this.routes.Add(new ServiceRoute("commodityinstrumenttype", hostFactory, typeof(Legacy.CommodityInstrumentTypeService)));
            this.routes.Add(new ServiceRoute("bookdefault", hostFactory, typeof(Legacy.BookDefaultService)));
            this.routes.Add(new ServiceRoute("dimension", hostFactory, typeof(Legacy.DimensionService)));
            this.routes.Add(new ServiceRoute("exchange", hostFactory, typeof(Legacy.ExchangeService)));
            this.routes.Add(new ServiceRoute("hierarchy", hostFactory, typeof(Legacy.HierarchyService)));
            this.routes.Add(new ServiceRoute("instrumenttype", hostFactory, typeof(Legacy.InstrumentTypeService)));
            this.routes.Add(new ServiceRoute("instrumenttypeoverride", hostFactory, typeof(Legacy.InstrumentTypeOverrideService)));
            this.routes.Add(new ServiceRoute("location", hostFactory, typeof(Legacy.LocationService)));
            this.routes.Add(new ServiceRoute("market", hostFactory, typeof(Legacy.MarketService)));
            this.routes.Add(new ServiceRoute("party", hostFactory, typeof(Legacy.PartyService)));
            this.routes.Add(new ServiceRoute("partyaccountability", hostFactory, typeof(Legacy.PartyAccountabilityService)));
            this.routes.Add(new ServiceRoute("partycommodity", hostFactory, typeof(Legacy.PartyCommodityService)));
            this.routes.Add(new ServiceRoute("partyoverride", hostFactory, typeof(Legacy.PartyOverrideService)));
            this.routes.Add(new ServiceRoute("partyrole", hostFactory, typeof(Legacy.PartyRoleService)));
            this.routes.Add(new ServiceRoute("partyroleaccountability", hostFactory, typeof(Legacy.PartyRoleAccountabilityService)));
            this.routes.Add(new ServiceRoute("person", hostFactory, typeof(Legacy.PersonService)));
            this.routes.Add(new ServiceRoute("portfolio", hostFactory, typeof(Legacy.PortfolioService)));
            this.routes.Add(new ServiceRoute("portfoliohierarchy", hostFactory, typeof(Legacy.PortfolioHierarchyService)));
            this.routes.Add(new ServiceRoute("product", hostFactory, typeof(Legacy.ProductService)));
            this.routes.Add(new ServiceRoute("productcurve", hostFactory, typeof(Legacy.ProductCurveService)));
            this.routes.Add(new ServiceRoute("productscota", hostFactory, typeof(Legacy.ProductScotaService)));
            this.routes.Add(new ServiceRoute("producttype", hostFactory, typeof(Legacy.ProductTypeService)));
            this.routes.Add(new ServiceRoute("producttypeinstance", hostFactory, typeof(Legacy.ProductTypeInstanceService)));
            this.routes.Add(new ServiceRoute("referencedata", hostFactory, typeof(Legacy.ReferenceDataService)));
            this.routes.Add(new ServiceRoute("settlementcontact", hostFactory, typeof(Legacy.SettlementContactService)));
            this.routes.Add(new ServiceRoute("shape", hostFactory, typeof(Legacy.ShapeService)));
            this.routes.Add(new ServiceRoute("shapeday", hostFactory, typeof(Legacy.ShapeDayService)));
            this.routes.Add(new ServiceRoute("shapeelement", hostFactory, typeof(Legacy.ShapeElementService)));
            this.routes.Add(new ServiceRoute("shippercode", hostFactory, typeof(Legacy.ShipperCodeService)));
            this.routes.Add(new ServiceRoute("sourcesystem", hostFactory, typeof(Legacy.SourceSystemService)));
            this.routes.Add(new ServiceRoute("unit", hostFactory, typeof(Legacy.UnitService)));
            this.routes.Add(new ServiceRoute("vessel", hostFactory, typeof(Legacy.VesselService)));
            this.routes.Add(new ServiceRoute("FeeType", hostFactory, typeof(Legacy.FeeTypeService)));
            this.routes.Add(new ServiceRoute("CommodityFeeType", hostFactory, typeof(Legacy.CommodityFeeTypeService)));
            this.routes.Add(new ServiceRoute("tenor", hostFactory, typeof(Legacy.TenorService)));
            this.routes.Add(new ServiceRoute("tenortype", hostFactory, typeof(Legacy.TenorTypeService)));
            this.routes.Add(new ServiceRoute("producttenortype", hostFactory, typeof(Legacy.ProductTenorTypeService)));
            this.routes.Add(new ServiceRoute("legalentity", hostFactory, typeof(Legacy.LegalEntityService)));


            this.routes.Add(new ServiceRoute("opennexus/agreement", hostFactory, typeof(AgreementService)));
            this.routes.Add(new ServiceRoute("opennexus/book", hostFactory, typeof(BookService)));
            this.routes.Add(new ServiceRoute("opennexus/broker", hostFactory, typeof(BrokerService)));
            this.routes.Add(new ServiceRoute("opennexus/brokercommodity", hostFactory, typeof(BrokerCommodityService)));
            this.routes.Add(new ServiceRoute("opennexus/brokerrate", hostFactory, typeof(BrokerRateService)));
            this.routes.Add(new ServiceRoute("opennexus/calendar", hostFactory, typeof(CalendarService)));          
            this.routes.Add(new ServiceRoute("opennexus/businessunit", hostFactory, typeof(BusinessUnitService)));
            this.routes.Add(new ServiceRoute("opennexus/counterparty", hostFactory, typeof(CounterpartyService)));
            this.routes.Add(new ServiceRoute("opennexus/curve", hostFactory, typeof(CurveService)));
            this.routes.Add(new ServiceRoute("opennexus/commodity", hostFactory, typeof(CommodityService)));
            this.routes.Add(new ServiceRoute("opennexus/commodityinstrumenttype", hostFactory, typeof(CommodityInstrumentTypeService)));
            this.routes.Add(new ServiceRoute("opennexus/bookdefault", hostFactory, typeof(BookDefaultService)));
            this.routes.Add(new ServiceRoute("opennexus/dimension", hostFactory, typeof(DimensionService)));
            this.routes.Add(new ServiceRoute("opennexus/exchange", hostFactory, typeof(ExchangeService)));
            this.routes.Add(new ServiceRoute("opennexus/hierarchy", hostFactory, typeof(HierarchyService)));
            this.routes.Add(new ServiceRoute("opennexus/instrumenttype", hostFactory, typeof(InstrumentTypeService)));
            this.routes.Add(new ServiceRoute("opennexus/instrumenttypeoverride", hostFactory, typeof(InstrumentTypeOverrideService)));
            this.routes.Add(new ServiceRoute("opennexus/location", hostFactory, typeof(LocationService)));
            this.routes.Add(new ServiceRoute("opennexus/market", hostFactory, typeof(MarketService)));
            this.routes.Add(new ServiceRoute("opennexus/party", hostFactory, typeof(PartyService)));
            this.routes.Add(new ServiceRoute("opennexus/partyaccountability", hostFactory, typeof(PartyAccountabilityService)));
            this.routes.Add(new ServiceRoute("opennexus/partycommodity", hostFactory, typeof(PartyCommodityService)));
            this.routes.Add(new ServiceRoute("opennexus/partyoverride", hostFactory, typeof(PartyOverrideService)));
            this.routes.Add(new ServiceRoute("opennexus/partyrole", hostFactory, typeof(PartyRoleService)));
            this.routes.Add(new ServiceRoute("opennexus/partyroleaccountability", hostFactory, typeof(PartyRoleAccountabilityService)));
            this.routes.Add(new ServiceRoute("opennexus/person", hostFactory, typeof(PersonService)));
            this.routes.Add(new ServiceRoute("opennexus/portfolio", hostFactory, typeof(PortfolioService)));
            this.routes.Add(new ServiceRoute("opennexus/portfoliohierarchy", hostFactory, typeof(PortfolioHierarchyService)));
            this.routes.Add(new ServiceRoute("opennexus/product", hostFactory, typeof(ProductService)));
            this.routes.Add(new ServiceRoute("opennexus/productcurve", hostFactory, typeof(ProductCurveService)));
            this.routes.Add(new ServiceRoute("opennexus/productscota", hostFactory, typeof(ProductScotaService)));
            this.routes.Add(new ServiceRoute("opennexus/producttype", hostFactory, typeof(ProductTypeService)));
            this.routes.Add(new ServiceRoute("opennexus/producttypeinstance", hostFactory, typeof(ProductTypeInstanceService)));
            this.routes.Add(new ServiceRoute("opennexus/referencedata", hostFactory, typeof(ReferenceDataService)));
            this.routes.Add(new ServiceRoute("opennexus/settlementcontact", hostFactory, typeof(SettlementContactService)));
            this.routes.Add(new ServiceRoute("opennexus/shape", hostFactory, typeof(ShapeService)));
            this.routes.Add(new ServiceRoute("opennexus/shapeday", hostFactory, typeof(ShapeDayService)));
            this.routes.Add(new ServiceRoute("opennexus/shapeelement", hostFactory, typeof(ShapeElementService)));
            this.routes.Add(new ServiceRoute("opennexus/shippercode", hostFactory, typeof(ShipperCodeService)));
            this.routes.Add(new ServiceRoute("opennexus/sourcesystem", hostFactory, typeof(SourceSystemService)));
            this.routes.Add(new ServiceRoute("opennexus/unit", hostFactory, typeof(UnitService)));
            this.routes.Add(new ServiceRoute("opennexus/vessel", hostFactory, typeof(VesselService)));
            this.routes.Add(new ServiceRoute("opennexus/FeeType", hostFactory, typeof(FeeTypeService)));
            this.routes.Add(new ServiceRoute("opennexus/CommodityFeeType", hostFactory, typeof(CommodityFeeTypeService)));
            this.routes.Add(new ServiceRoute("opennexus/tenor", hostFactory, typeof(TenorService)));
            this.routes.Add(new ServiceRoute("opennexus/tenortype", hostFactory, typeof(TenorTypeService)));
            this.routes.Add(new ServiceRoute("opennexus/producttenortype", hostFactory, typeof(ProductTenorTypeService)));
            this.routes.Add(new ServiceRoute("opennexus/legalentity", hostFactory, typeof(LegalEntityService)));
        }
    }
}