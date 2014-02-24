namespace EnergyTrading.MDM.Test
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using System.ServiceModel.Web;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.MDM.MappingService;

    [TestClass]
    public abstract class IntegrationTestBase
    {
        protected static string DateFormatString = "yyyy-MM-dd'T'HH:mm:ss.fffffffZ";
        protected static ObjectScript Script;
        protected static Dictionary<string, string> ServiceUrl;
        private static List<WebServiceHost> webServiceHosts = new List<WebServiceHost>();

        [AssemblyInitialize]
        public static void CreateServiceHost(TestContext context)
        {
            // TODO_IntegrationTest - add unique url for new entity
            ServiceUrl = new Dictionary<string, string>
                {
                    { "Person", "http://127.0.0.1:8000/" }, 
                    { "Party", "http://127.0.0.1:8001/" }, 
                    { "Commodity", "http://127.0.0.1:8002/" },
                    { "Location", "http://127.0.0.1:8003/" },
                    { "Calendar", "http://127.0.0.1:8004/" },
                    { "Market", "http://127.0.0.1:8005/" },
                    { "Product", "http://127.0.0.1:8006/" },
                    { "ProductType", "http://127.0.0.1:8007/" },
                    { "ProductTypeInstance", "http://127.0.0.1:8008/" },
                    { "TradeProduct", "http://127.0.0.1:8009/" },
                    { "LocationRole", "http://127.0.0.1:8010/" },
                    { "ShipperCode", "http://127.0.0.1:8012/" },
                    { "SourceSystem", "http://127.0.0.1:8013/" },
                    { "ReferenceData", "http://127.0.0.1:8014/" },
                    { "InstrumentType", "http://127.0.0.1:8015/" },
                    { "Portfolio", "http://127.0.0.1:8016/" },
                    { "Hierarchy", "http://127.0.0.1:8017/" },
                    { "PortfolioHierarchy", "http://127.0.0.1:8018/" },
                    { "PartyCommodity", "http://127.0.0.1:8019/" },
                    { "CommodityInstrumentType", "http://127.0.0.1:8021/" },
                    { "PartyRole", "http://127.0.0.1:8022/" },
                    { "Exchange", "http://127.0.0.1:8023/" },
                    { "Curve", "http://127.0.0.1:8024/" },
                    { "Broker", "http://127.0.0.1:8025/" },
                    { "Counterparty", "http://127.0.0.1:8026/" },
                    { "Dimension", "http://127.0.0.1:8027/" },
                    { "Unit", "http://127.0.0.1:8028/" },
                    { "PartyAccountability", "http://127.0.0.1:8029/" },
                    { "SettlementContact", "http://127.0.0.1:8030/" },
                    { "BusinessUnit", "http://127.0.0.1:8031/" },
                    { "ProductCurve", "http://127.0.0.1:8032/" },
                    { "InstrumentTypeOverride", "http://127.0.0.1:8033/" },
                    { "PartyOverride", "http://127.0.0.1:8034/" },
                    { "BrokerRate", "http://127.0.0.1:8035/" },
                    { "Vessel", "http://127.0.0.1:8036/" },
                    { "ProductScota", "http://127.0.0.1:8037/" },
                    { "BrokerCommodity", "http://127.0.0.1:8038/" },
                    { "Shape", "http://127.0.0.1:8039/" },
                    { "ShapeDay", "http://127.0.0.1:8040/" },
                    { "ShapeElement", "http://127.0.0.1:8041/" },
                    { "FeeType", "http://127.0.0.1:8042/" },
                    { "CommodityFeeType", "http://127.0.0.1:8043/" },
                    { "Tenor", "http://127.0.0.1:8044/" },
                    { "TenorType", "http://127.0.0.1:8045/" },
                    { "ProductTenorType", "http://127.0.0.1:8046/" },
                    { "LegalEntity", "http://127.0.0.1:8047/" },
                    { "Agreement", "http://127.0.0.1:8048/" },
                    { "Book", "http://127.0.0.1:8049/" },
                    { "BookDefault", "http://127.0.0.1:8050/" },
                    { "PartyRoleAccountability", "http://127.0.0.1:8051/" }
                };

            // TODO_IntegrationTest - add WebServiceHost for new entity
            webServiceHosts.Add(new WebServiceHost(typeof(AgreementService), new Uri(ServiceUrl["Agreement"])));
            webServiceHosts.Add(new WebServiceHost(typeof(BookService), new Uri(ServiceUrl["Book"])));
            webServiceHosts.Add(new WebServiceHost(typeof(BrokerRateService), new Uri(ServiceUrl["BrokerRate"])));
            webServiceHosts.Add(new WebServiceHost(typeof(PersonService), new Uri(ServiceUrl["Person"])));
            webServiceHosts.Add(new WebServiceHost(typeof(CommodityService), new Uri(ServiceUrl["Commodity"])));
            webServiceHosts.Add(new WebServiceHost(typeof(LocationService), new Uri(ServiceUrl["Location"])));
            webServiceHosts.Add(new WebServiceHost(typeof(PartyService), new Uri(ServiceUrl["Party"])));
            webServiceHosts.Add(new WebServiceHost(typeof(CalendarService), new Uri(ServiceUrl["Calendar"])));
            webServiceHosts.Add(new WebServiceHost(typeof(MarketService), new Uri(ServiceUrl["Market"])));
            webServiceHosts.Add(new WebServiceHost(typeof(ProductService), new Uri(ServiceUrl["Product"])));
            webServiceHosts.Add(new WebServiceHost(typeof(ProductTypeService), new Uri(ServiceUrl["ProductType"])));
            webServiceHosts.Add(new WebServiceHost(typeof(ProductTypeInstanceService), new Uri(ServiceUrl["ProductTypeInstance"])));
            webServiceHosts.Add(new WebServiceHost(typeof(LocationRoleService), new Uri(ServiceUrl["LocationRole"])));
            webServiceHosts.Add(new WebServiceHost(typeof(ShipperCodeService), new Uri(ServiceUrl["ShipperCode"])));
            webServiceHosts.Add(new WebServiceHost(typeof(SourceSystemService), new Uri(ServiceUrl["SourceSystem"])));
            webServiceHosts.Add(new WebServiceHost(typeof(ReferenceDataService), new Uri(ServiceUrl["ReferenceData"])));
            webServiceHosts.Add(new WebServiceHost(typeof(InstrumentTypeService), new Uri(ServiceUrl["InstrumentType"])));
            webServiceHosts.Add(new WebServiceHost(typeof(PortfolioService), new Uri(ServiceUrl["Portfolio"])));
            webServiceHosts.Add(new WebServiceHost(typeof(HierarchyService), new Uri(ServiceUrl["Hierarchy"])));
            webServiceHosts.Add(new WebServiceHost(typeof(PortfolioHierarchyService), new Uri(ServiceUrl["PortfolioHierarchy"])));
            webServiceHosts.Add(new WebServiceHost(typeof(PartyCommodityService), new Uri(ServiceUrl["PartyCommodity"])));
            webServiceHosts.Add(new WebServiceHost(typeof(CommodityInstrumentTypeService), new Uri(ServiceUrl["CommodityInstrumentType"])));
            webServiceHosts.Add(new WebServiceHost(typeof(PartyRoleService), new Uri(ServiceUrl["PartyRole"])));
            webServiceHosts.Add(new WebServiceHost(typeof(ExchangeService), new Uri(ServiceUrl["Exchange"])));
            webServiceHosts.Add(new WebServiceHost(typeof(CurveService), new Uri(ServiceUrl["Curve"])));
            webServiceHosts.Add(new WebServiceHost(typeof(BrokerService), new Uri(ServiceUrl["Broker"])));
            webServiceHosts.Add(new WebServiceHost(typeof(CounterpartyService), new Uri(ServiceUrl["Counterparty"])));
            webServiceHosts.Add(new WebServiceHost(typeof(BusinessUnitService), new Uri(ServiceUrl["BusinessUnit"])));
            webServiceHosts.Add(new WebServiceHost(typeof(DimensionService), new Uri(ServiceUrl["Dimension"])));
            webServiceHosts.Add(new WebServiceHost(typeof(UnitService), new Uri(ServiceUrl["Unit"])));
            webServiceHosts.Add(new WebServiceHost(typeof(PartyAccountabilityService), new Uri(ServiceUrl["PartyAccountability"])));
            webServiceHosts.Add(new WebServiceHost(typeof(SettlementContactService), new Uri(ServiceUrl["SettlementContact"])));
            webServiceHosts.Add(new WebServiceHost(typeof(ProductCurveService), new Uri(ServiceUrl["ProductCurve"])));
            webServiceHosts.Add(new WebServiceHost(typeof(InstrumentTypeOverrideService), new Uri(ServiceUrl["InstrumentTypeOverride"])));
            webServiceHosts.Add(new WebServiceHost(typeof(PartyOverrideService), new Uri(ServiceUrl["PartyOverride"])));
            webServiceHosts.Add(new WebServiceHost(typeof(VesselService), new Uri(ServiceUrl["Vessel"])));
            webServiceHosts.Add(new WebServiceHost(typeof(ProductScotaService), new Uri(ServiceUrl["ProductScota"])));
            webServiceHosts.Add(new WebServiceHost(typeof(BrokerCommodityService), new Uri(ServiceUrl["BrokerCommodity"])));
            webServiceHosts.Add(new WebServiceHost(typeof(ShapeService), new Uri(ServiceUrl["Shape"])));
            webServiceHosts.Add(new WebServiceHost(typeof(ShapeDayService), new Uri(ServiceUrl["ShapeDay"])));
            webServiceHosts.Add(new WebServiceHost(typeof(ShapeElementService), new Uri(ServiceUrl["ShapeElement"])));
            webServiceHosts.Add(new WebServiceHost(typeof(FeeTypeService), new Uri(ServiceUrl["FeeType"])));
            webServiceHosts.Add(new WebServiceHost(typeof(CommodityFeeTypeService), new Uri(ServiceUrl["CommodityFeeType"])));
            webServiceHosts.Add(new WebServiceHost(typeof(TenorService), new Uri(ServiceUrl["Tenor"])));
            webServiceHosts.Add(new WebServiceHost(typeof(TenorTypeService), new Uri(ServiceUrl["TenorType"])));
            webServiceHosts.Add(new WebServiceHost(typeof(ProductTenorTypeService), new Uri(ServiceUrl["ProductTenorType"])));
            webServiceHosts.Add(new WebServiceHost(typeof(LegalEntityService), new Uri(ServiceUrl["LegalEntity"])));
            webServiceHosts.Add(new WebServiceHost(typeof(BookDefaultService), new Uri(ServiceUrl["BookDefault"])));
            webServiceHosts.Add(new WebServiceHost(typeof(PartyRoleAccountabilityService), new Uri(ServiceUrl["PartyRoleAccountability"])));

            Script = new ObjectScript();
            Script.RunScript();

            var global = new GlobalMock();
            global.Application_Start();

            foreach(var host in webServiceHosts)
            {
                IncludeExceptionDetailInFaults(host);

                host.Open();
            }
        }

        private static void IncludeExceptionDetailInFaults(ServiceHostBase host)
        {
            var behavior = host.Description.Behaviors.Find<ServiceDebugBehavior>();

            if (behavior == null)
            {
                behavior = new ServiceDebugBehavior();
                host.Description.Behaviors.Add(behavior);
            }

            behavior.IncludeExceptionDetailInFaults = true;
        }

        [AssemblyCleanup]
        public static void CloseServiceHost()
        {
            foreach(var host in webServiceHosts)
            {
                host.Close();
            }
        }
    }
}