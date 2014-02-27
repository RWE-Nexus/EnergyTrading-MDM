namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Collections.Generic;

    using EnergyTrading;
    using EnergyTrading.Data;
    using RWEST.Nexus.MDM;

    public static class ObjectMother
    {
        public static T Create<T>()
            where T : IIdentifiable
        {
            var value = Create(typeof(T).Name);

            return (T)value;
        }

        public static IIdentifiable Create(string name)
        {
            switch (name)
            {
                // TODO_UnitTestGeneration - Add new entity here
                case "Agreement":
                    return new Agreement
                    {
                        Name = "Agreement",
                        PaymentTerms = "PaymentTerms"
                    };

                case "Book":
                    return new Book { Name = "Book" };

                case "Broker":
                    var broker = new Broker { PartyRoleType = "Broker" };
                    var brokerDetails = Create<BrokerDetails>();
                    broker.Party = Create<Party>();
                    brokerDetails.PartyRole = broker;
                    broker.Details.Add(brokerDetails);
                    return broker;

                case "BrokerCommodity":
                    return new BrokerCommodity
                    {
                        Name = "BrokerCommodity" + Guid.NewGuid(),
                        Broker = Create<Broker>(),
                        Commodity = Create<Commodity>()
                    };

                case "BrokerDetails":
                    return new BrokerDetails
                    {
                        Name = "Name" + Guid.NewGuid(),
                        Fax = "01302111111",
                        Phone = "01302222222",
                        Rate = 1.1m
                    };

                case "BrokerRate":
                        var brokerdetails = Create<BrokerRateDetails>();
                        var brokerrate = new BrokerRate();
                        brokerrate.Details.Add(brokerdetails);
                        brokerdetails.BrokerRate = brokerrate;
                        return brokerrate;
                        
                case "BrokerRateDetails":
                    return new BrokerRateDetails
                    {
                        Broker = Create<Broker>(),
                        Desk = Create<PartyRole>(),
                        CommodityInstrumentType = Create<CommodityInstrumentType>(),
                        Location = Create<Location>(),
                        ProductType = Create<ProductType>(),
                        PartyAction = (int)RWEST.Nexus.MDM.Contracts.PartyAction.Initiator,
                        Rate = 3.4m,
                        RateType = "per unit",
                        Currency = "GBP"
                    };

                case "BusinessUnit":
                    var businessUnit = new BusinessUnit { PartyRoleType = "BusinessUnit" };
                    var businessUnitDetails = Create<BusinessUnitDetails>();
                    businessUnit.Party = Create<Party>();
                    businessUnitDetails.PartyRole = businessUnit;
                    businessUnit.Details.Add(businessUnitDetails);
                    return businessUnit;

                case "BusinessUnitDetails":
                    return new BusinessUnitDetails
                    {
                        Name = "Name" + Guid.NewGuid(),
                        Fax = "1234123413",
                        Phone = "34563456",
                        AccountType = "TRADING",
                        Address = "12 Hanover street, Germany",
                        Status = "Active",
                        TaxLocation = Create<Location>()
                    };

                case "Counterparty":
                    var counterparty = new Counterparty { PartyRoleType = "Counterparty" };
                    var counterpartyDetails = Create<CounterpartyDetails>();
                    counterparty.Party = Create<Party>();
                    counterpartyDetails.PartyRole = counterparty;
                    counterparty.Details.Add(counterpartyDetails);
                    return counterparty;

                case "CounterpartyDetails":
                    return new CounterpartyDetails
                    {
                        Name = "Name" + Guid.NewGuid(),
                        Fax = "01302111111",
                        Phone = "01302222222",
                        ShortName = "sh",
                        //TaxLocation = Create<Location>()
                    };

                case "Calendar":
                    var calendar = new Calendar
                    {
                        Name = "Calendar" + Guid.NewGuid(),
                        Days = new List<CalendarDay> { new CalendarDay { Date = DateTime.Now, DayType = 0 } }
                    };
                    calendar.Days[0].Calendar = calendar;
                    return calendar;

                case "Commodity":
                    return new Commodity
                    {
                        Name = "Commodity" + Guid.NewGuid(),
                        Parent = new Commodity { Name = "ParentCommodity" + Guid.NewGuid() },
                        MassEnergyUnits = Create<Unit>(),
                        MassEnergyValue = 1400,
                        VolumeEnergyUnits = Create<Unit>(),
                        VolumeEnergyValue = 29.5m,
                        VolumetricDensityUnits = Create<Unit>(),
                        VolumetricDensityValue = 2344,
                        DeliveryRate = "Hour"
                    };

                case "CommodityInstrumentType":
                    return new CommodityInstrumentType
                    {
                        Commodity = Create<Commodity>(),
                        InstrumentType = Create<InstrumentType>(),
                        InstrumentDelivery = "physical"
                    };

                case "Curve":
                    return new Curve
                    {
                        Name = "Curve" + Guid.NewGuid(),
                        Type = "Forward",
                        Currency = "GBP",
                        Location = Create<Location>(),
                        Commodity = Create<Commodity>(),
                        Originator= Create<Party>(),
                        CommodityUnit = "ton",
                        DefaultSpread = .005m
                    };

                case "BookDefault":
                    return new BookDefault
                    {
                        Name = "BookDefault" + Guid.NewGuid(),
                        Book = Create<Book>(),
                        Desk = Create<PartyRole>(),
                        GfProductMapping = "GfProductMapping",
                        Trader = Create<Person>(),
                        DefaultType = "DefaultType",
                        PartyRole = Create<PartyRole>()
                    };

                case "Dimension":
                    return new Dimension
                    {
                        Name = "Dimension" + Guid.NewGuid(),
                        Description = "Blah Blah Dimension description",
                        ElectricCurrentDimension = 1,
                        LengthDimension = 2,
                        LuminousIntensityDimension = 3,
                        TemperatureDimension = 4,
                        MassDimension = 5,
                        TimeDimension = 6
                    };

                case "Exchange":
                    var exchange = new Exchange { PartyRoleType = "Exchange", Party = Create<Party>() };
                    var exchangeDetails = Create<ExchangeDetails>();
                    exchangeDetails.PartyRole = exchange;
                    exchange.Details.Add(exchangeDetails);
                    return exchange;

                case "ExchangeDetails":
                    return
                    new ExchangeDetails
                    {
                        Name = "Name" + Guid.NewGuid(),
                        Phone = "118118"
                    };

                case "Hierarchy":
                    return new Hierarchy
                    {
                        Name = "Hierarchy" + Guid.NewGuid(),
                    };

                case "InstrumentType":
                    return new InstrumentType
                    {
                        Name = "InstrumentType" + Guid.NewGuid(),
                    };

                case "InstrumentTypeOverride":
                    return new InstrumentTypeOverride
                    {
                        Name = "InstrumentTypeOverride" + Guid.NewGuid(),
                        ProductType = Create<ProductType>(),
                        Broker = Create<Broker>(),
                        CommodityInstrumentType = Create<CommodityInstrumentType>(),
                        InstrumentSubType = "Financial" + Guid.NewGuid(),
                        ProductTenorType = Create<ProductTenorType>(),
                    };

                case "LegalEntity":
                    var legalEntity = new LegalEntity { PartyRoleType = "LegalEntity" };
                    var legalEntityDetails = Create<LegalEntityDetails>();
                    legalEntity.Party = Create<Party>();
                    legalEntityDetails.PartyRole = legalEntity;
                    legalEntity.Details.Add(legalEntityDetails);
                    return legalEntity;

                case "LegalEntityDetails":
                    return new LegalEntityDetails
                    {
                        Name = "Name" + Guid.NewGuid(),
                        RegisteredName = "RegisteredName",
                        RegistrationNumber = "RegistrationNumber",
                        Address = "123 Fake St",
                        Website = "http://test.com",
                        CountryOfIncorporation = "Germany",
                        Email = "foo@bar.com",
                        Fax = "020 1234 5678",
                        Phone = "020 3469 1256",
                        PartyStatus = "Active",
                        CustomerAddress = "456 Wrong Road",
                        InvoiceSetup = "Customer",
                        VendorAddress = "789 Right Road"
                    };

                case "Location":
                    return new Location
                    {
                        Type = "LocationType" + Guid.NewGuid(),
                        Name = "Location" + Guid.NewGuid(),
                    };

                case "LocationRole":
                    return new LocationRole
                    {
                        Type = Create<LocationRoleType>(),
                        Location = Create<Location>(),
                    };

                case "LocationRoleType":
                    return new LocationRoleType
                    {
                        Name = "LocationRoleType" + Guid.NewGuid(),
                    };

                case "Market":
                    return new Market
                    {
                        Name = "Name" + Guid.NewGuid(),
                        Location = Create<Location>(),
                        Commodity = Create<Commodity>(),
                        Calendar = Create<Calendar>(),
                        Currency = "EUR",
                        TradeUnits = "kWh",
                        TradeUnitsRate = "Day",
                        NominationUnits = "kWh",
                        PriceUnits = "EUR",
                        DeliveryRate = "Day",
                        IncoTerms = "FOB"
                    };

                case "Party":
                    var partyDetails = Create<PartyDetails>();
                    var party = new Party();
                    party.Details.Add(partyDetails);
                    partyDetails.Party = party;
                    return party;

                case "PartyAccountability":
                    var partyAccountability = new PartyAccountability
                    {
                        Name = "Name" + Guid.NewGuid(),
                        PartyAccountabilityType = "PartyAccountability",
                        SourceParty = Create<Party>(),
                        TargetParty = Create<Party>()
                    };
                    return partyAccountability;

                case "PartyDetails":
                    var ptDetails = new PartyDetails
                    {
                        Name = "Name" + Guid.NewGuid(),
                        Phone = "020 8823 1234",
                        Fax = "020 834 1237",
                        Role = "Trader"
                    };
                    return ptDetails;

                case "PartyOverride":
                    return new PartyOverride
                    {
                        Broker = Create<Broker>(),
                        CommodityInstrumentType = Create<CommodityInstrumentType>(),
                        MappingValue = "MappingValue" + Guid.NewGuid(),
                        Party = Create<Party>(),
                    };

                case "PartyRole":
                    var partyRole = new PartyRole { PartyRoleType = "SomeRole", Party = Create<Party>() };
                    partyRole.Details.Add(Create<PartyRoleDetails>());
                    return partyRole;

                case "PartyRoleAccountability":
                    var partyRoleAccountability = new PartyRoleAccountability
                    {
                        Name = "Name" + Guid.NewGuid(),
                        PartyRoleAccountabilityType = "Customer",
                        SourcePartyRole = Create<PartyRole>(),
                        TargetPartyRole = Create<PartyRole>()
                    };
                    return partyRoleAccountability;

                case "PartyRoleDetails":
                    return
                    new PartyRoleDetails
                    {
                        Name = "Name" + Guid.NewGuid(),
                    };

                case "Person":
                    var personDetails = Create<PersonDetails>();
                    var person = new Person();
                    person.Details.Add(personDetails);
                    return person;

                case "PersonDetails":
                    var psDetails = new PersonDetails
                    {
                        FirstName = "Firstname" + Guid.NewGuid(),
                        LastName = "Lastname" + Guid.NewGuid(),
                        Email = "test@test.com"
                    };
                    return psDetails;

                case "Portfolio":
                    return new Portfolio
                    {
                        Name = "Test Portfolio" + Guid.NewGuid(),
                        PortfolioType = ("Test Portfolio Type" + Guid.NewGuid()).Substring(0, 50),
                        BusinessUnit = Create<PartyRole>()
                    };

                case "PortfolioHierarchy":
                    return new PortfolioHierarchy
                    {
                        ChildPortfolio = Create<Portfolio>(),
                        ParentPortfolio = Create<Portfolio>(),
                        Hierarachy = Create<Hierarchy>()
                    };

                case "Product":
                    return new Product
                    {
                        Name = "Product" + Guid.NewGuid(),
                        Market = Create<Market>(),
                        Shape = Create<Shape>(),
                        CommodityInstrumentType = Create<CommodityInstrumentType>(),
                        DefaultCurve = Create<Curve>(),
                        LotSize = 3,
                        CalendarRule = "abcCalendarRule",
                        InstrumentSubType = "Fixed"
                    };

                case "ProductCurve":
                    var productCurve = new ProductCurve
                    {
                        Name = "ProductCurveName" + Guid.NewGuid(),
                        Curve = Create<Curve>(),
                        Product = Create<Product>(),
                        ProductCurveType = "ProductCurveType" + Guid.NewGuid(),
                        ProjectionMethod = "Monthly" + Guid.NewGuid()
                    };
                    return productCurve;

                case "ProductScota":
                    var productScota = new ProductScota
                    {
                        Name = "ProductScotaName" + Guid.NewGuid(),
                        Product = Create<Product>(),
                        ScotaDeliveryPoint = Create<Location>(),
                        ScotaOrigin = Create<Location>(),
                        ScotaContract = "ScotaContract" + Guid.NewGuid(),
                        ScotaRss = "ScotaRss" + Guid.NewGuid(),
                        ScotaVersion = "ScotaVersion" + Guid.NewGuid()
                    };
                    return productScota;

                case "ProductTenorType":
                    return new ProductTenorType
                    {
                        Product = Create<Product>(),
                        TenorType = Create<TenorType>(),
                    };

                case "ProductType":
                    return new ProductType
                    {
                        Name = "ProductType" + Guid.NewGuid(),
                        ShortName = "PT" + Guid.NewGuid(),
                        Product = Create<Product>(),
                        DeliveryPeriod = "xxx",
                        DeliveryRangeType = Guid.NewGuid().ToString(),
                    };

                case "ProductTypeInstance":
                    return new ProductTypeInstance
                    {
                        Name = "ProductType" + Guid.NewGuid(),
                        ShortName = "PTI" + Guid.NewGuid(),
                        ProductType = Create<ProductType>(),
                        Delivery = DateRange.MaxDateRange,
                        DeliveryPeriod = "xxx",
                        Traded = DateRange.MaxDateRange,
                    };

                case "SettlementContact":
                    var settlementContact = new SettlementContact
                    {
                        Name = "Name" + Guid.NewGuid(),
                        PartyAccountabilityType = "SettlementContact",
                        SourceParty = Create<Party>(),
                        TargetParty = Create<Party>(),
                        CommodityInstrumentType = Create<CommodityInstrumentType>(),
                        Location = Create<Location>()
                    };
                    return settlementContact;

                case "Shape":
                    return new Shape
                    {
                        Name = "Shape" + Guid.NewGuid(),
                    };

                case "ShapeDay":
                    return new ShapeDay
                    {
                        DayType = "Weekday",
                        Shape = Create<Shape>(),
                        ShapeElement = Create<ShapeElement>()
                    };

                case "ShapeElement":
                    return new ShapeElement
                    {
                        Name = "ShapeElement" + Guid.NewGuid(),
                        Period = DateRange.MaxDateRange,
                    };

                case "ShipperCode":
                    return new ShipperCode
                    {
                        Location = Create<Location>(),
                        Party = Create<Party>(),
                        Code = "Code" + Guid.NewGuid()
                    };

                case "SourceSystem":
                    return new SourceSystem
                    {
                        Name = "SourceSystem" + Guid.NewGuid()
                    };

                case "Tenor":
                    return new Tenor
                    {
                        Name = "TENOR" + Guid.NewGuid(),
                        ShortName = "tenor",
                        TenorType = Create<TenorType>(),
                        IsRelative = false,
                        DeliveryPeriod = "deliveryPeriod",
                        DeliveryRangeType = "deliveryRangeType",
                        Delivery = DateRange.MaxDateRange,
                        Traded = DateRange.MaxDateRange,
                    };

                case "TenorType":
                    return new TenorType
                    {
                        Name = "TENOR_TYPE" + Guid.NewGuid(),
                        ShortName = "tenorType",
                        //IsRelative = true,
                        //DeliveryPeriod = "deliveryPeriod",
                        //DeliveryRangeType = "deliveryRangeType",
                        //Traded = DateRange.MaxDateRange,
                    };

                case "Unit":
                    return new Unit
                    {
                        Name = "testUnit" + Guid.NewGuid().ToString(),
                        Description = "Test Thermal unit",
                        ConversionConstant = new decimal(2.3234),
                        ConversionFactor = new decimal(7.5000045),
                        Dimension = Create<Dimension>(),
                        Symbol = "£<=>$"
                    };

                case "Vessel":
                    return new Vessel
                    {
                        Name = "testVessel" + Guid.NewGuid().ToString()
                    };

                case "FeeType":
                    return new FeeType
                    {
                        Name = "FeeType" + Guid.NewGuid()
                    };

                case "CommodityFeeType":
                    return new CommodityFeeType
                    {
                        Commodity = Create<Commodity>(),
                        FeeType = Create<FeeType>(),
                    };


                default:
                    throw new NotImplementedException("No OM for " + name);
            }
        }
    }
}