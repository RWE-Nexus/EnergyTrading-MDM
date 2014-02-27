using System.Web.Mvc;

//using EnergyTrading.MDM.MappingService2.Filters;

namespace EnergyTrading.MDM.MappingService2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}