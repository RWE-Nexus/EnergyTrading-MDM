namespace EnergyTrading.MDM.Test
{
    using EnergyTrading.MDM.MappingService;

        public class GlobalMock : Global
        {
            public void Application_Start()
            {
                base.Application_Start(this, null);
            }
        }
}