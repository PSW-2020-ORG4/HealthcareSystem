using Model.Manager;
using System.Collections.Generic;

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
