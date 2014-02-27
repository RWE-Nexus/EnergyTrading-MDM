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
        public SourceSystemData SourceSystemData;

        public SourceSystemDataChecker SourceSystemDataChecker;

        public DateTime baseDate = SystemTime.UtcNow().AddDays(1);

        private SourceSystem endur;

        private SourceSystem gastar;

        private SourceSystem trayport;

        public static DbSetRepository Repository;

        static ObjectScript()
        {
            Repository = new DbSetRepository(new DbContextProvider(() => new MappingContext()));
        }

        public void RunScript()
        {

            using (var t = new TransactionScope(TransactionScopeOption.Required))
            {
		            this.SourceSystemData = new SourceSystemData(Repository);
            this.SourceSystemDataChecker = new SourceSystemDataChecker();
            
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