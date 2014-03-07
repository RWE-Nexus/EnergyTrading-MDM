namespace EnergyTrading.MDM.Test
{
    using EnergyTrading.MDM.ServiceHost.Wcf.Nexus;

    public class GlobalMock : Global
        {
            public void Application_Start()
            {
                base.Application_Start(this, null);
            }
        }
}