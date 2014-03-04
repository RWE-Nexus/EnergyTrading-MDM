//using EnergyTrading.MDM.MappingService2.Filters;

namespace EnergyTrading.MDM.ServiceHost.WebApi.Nexus
{
    using System.Web.Mvc;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}