using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.PlacementInARoomAndRenovationPeriod
{
    public interface IRenovationPeriodService
    {
        void AddRenovationPeriod(RenovationPeriod renovationPeriod);
        void DeleteRenovationPeriod(int roomNumber);
        List<RenovationPeriod> ViewRenovations();
        RenovationPeriod ViewRenovationByRoomNumber(int roomNumber);
        void UpdateRenovationPeriod(RenovationPeriod renovationPeriod);
    }
}
