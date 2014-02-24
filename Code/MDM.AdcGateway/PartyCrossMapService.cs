using System.Linq;
using System.Linq.Dynamic;
using MDM.Sync.Synchronizers.Adc;

namespace MDM.AdcGateway
{
    public interface IPartyCrossMapService
    {
        IQueryable FetchData();
    }

    public class PartyCrossMapService : IPartyCrossMapService
    {
        private PartyCrossMapDataContext dataContext;
        private IQueryable partyCrossMap;

        public PartyCrossMapService()
        {
            dataContext = new PartyCrossMapDataContext();
        }

        public IQueryable FetchData()
        {
            partyCrossMap = from m in dataContext.PartyCrossMaps
                         select m;
           
            return partyCrossMap;
        }
    }
}