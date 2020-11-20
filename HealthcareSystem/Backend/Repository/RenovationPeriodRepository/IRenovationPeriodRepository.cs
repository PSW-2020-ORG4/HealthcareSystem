using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.RenovationPeriodRepository
{
    public interface IRenovationPeriodRepository
    {
        List<RenovationPeriod> GetAllRenovationPeriod();
        RenovationPeriod GetRenovationPeriodByRoomNumber(int roomNumber);
        void UpdateRenovationPeriod(RenovationPeriod renovationPeriod);
        void DeleteRenovationPeriod(int roomNumber);
        void AddRenovationPeriod(RenovationPeriod renovationPeriod);
    }
}
