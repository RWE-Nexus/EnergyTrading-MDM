namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Collections.Generic;
    using System.Transactions;

    using EnergyTrading;
    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;

    public class ObjectScript
    {
        public CalendarData CalendarData;

        public CalendarDataChecker CalendarDataChecker;

        public CommodityData CommodityData;

        public CommodityDataChecker CommodityDataChecker;

        public InstrumentTypeData InsturmentDataChecker;


        public LocationData LocationData;

        public LocationDataChecker LocationDataChecker;

        public LocationRoleData LocationRoleData;

        public LocationRoleDataChecker LocationRoleDataChecker;

        public MarketData MarketData;

        public MarketDataChecker MarketDataChecker;
        public PartyData PartyData;

        public PartyDataChecker PartyDataChecker;
        public PersonData PersonData;

        public PersonDataChecker PersonDataChecker;

        public ProductData ProductData;

        public ProductDataChecker ProductDataChecker;

        public ProductTypeData ProductTypeData;

        public ProductTypeDataChecker ProductTypeDataChecker;

        public ProductTypeInstanceData ProductTypeInstanceData;

        public ProductTypeInstanceDataChecker ProductTypeInstanceDataChecker;

        public ShipperCodeData ShipperCodeData;

        public ShipperCodeDataChecker ShipperCodeDataChecker;
        public SourceSystemData SourceSystemData;

        public SourceSystemDataChecker SourceSystemDataChecker;

        public DateTime baseDate = SystemTime.UtcNow().AddDays(1);

        private SourceSystem endur;

        private SourceSystem gastar;

        private SourceSystem trayport;
        public InstrumentTypeData InstrumentTypeData;

        public FeeTypeData FeeTypeData;

        public FeeTypeDataChecker FeeTypeDataChecker;

        public InstrumentTypeDataChecker InstrumentTypeDataChecker;

        public static DbSetRepository Repository;

        static ObjectScript()
        {
            Repository = new DbSetRepository(new DbContextProvider(() => new MappingContext()));
        }

        public void RunScript()
        {

            using (var t = new TransactionScope(TransactionScopeOption.Required))
            {
                this.PersonData = new PersonData(Repository);
                this.PersonDataChecker = new PersonDataChecker();
                this.PartyData = new PartyData(Repository);
                this.PartyDataChecker = new PartyDataChecker();
		            this.CalendarData = new CalendarData(Repository);
            this.CalendarDataChecker = new CalendarDataChecker();
		            this.CommodityData = new CommodityData(Repository);
            this.CommodityDataChecker = new CommodityDataChecker();


		    this.LocationData = new LocationData(Repository);
            this.LocationDataChecker = new LocationDataChecker();


                this.InstrumentTypeData = new InstrumentTypeData(Repository);
                this.InstrumentTypeDataChecker = new InstrumentTypeDataChecker();

		            this.LocationRoleData = new LocationRoleData(Repository);
            this.LocationRoleDataChecker = new LocationRoleDataChecker();
		            this.MarketData = new MarketData(Repository);
            this.MarketDataChecker = new MarketDataChecker();
		            this.ProductData = new ProductData(Repository);
            this.ProductDataChecker = new ProductDataChecker();
		            this.ProductTypeData = new ProductTypeData(Repository);
            this.ProductTypeDataChecker = new ProductTypeDataChecker();
		            this.ProductTypeInstanceData = new ProductTypeInstanceData(Repository);
            this.ProductTypeInstanceDataChecker = new ProductTypeInstanceDataChecker();
		            this.SourceSystemData = new SourceSystemData(Repository);
            this.SourceSystemDataChecker = new SourceSystemDataChecker();
		            this.ShipperCodeData = new ShipperCodeData(Repository);
            this.ShipperCodeDataChecker = new ShipperCodeDataChecker();
            this.FeeTypeData = new FeeTypeData(Repository);
            this.FeeTypeDataChecker = new FeeTypeDataChecker(); 
            
            var z = new Zapper(Repository);
            z.Zap();

            this.CreateSystems();
            Repository.Flush();
            t.Complete();
            }
        }

        private void CreateSystems()
        {
            this.endur = new SourceSystem { Name = "Endur" };
            this.gastar = new SourceSystem { Name = "Gastar" };
            this.trayport = new SourceSystem { Name = "Trayport" };
            var spreadsheet = new SourceSystem { Name = "Spreadsheet" };

            Repository.Add(this.endur);
            Repository.Add(this.trayport);
            Repository.Add(this.gastar);
            Repository.Add(spreadsheet);
            Repository.Flush();
        }
    }
}