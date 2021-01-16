using Backend.Model.Manager;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Service.RenovationService
{
    public interface IBaseRenovationService
    {
        BaseRenovation AddBaseRenovation(BaseRenovation baseRenovation);
        List<BaseRenovation> GetAllBaseRenovations();
        BaseRenovation GetBaseRenovationById(int baseRenovationId);
        void DeleteBaseRenovation(int baseRenovationId);
        List<BaseRenovation> GetBaseRenovationByRoomNumber(int roomNumber);
        List<RenovationPeriod> GetAlternativeAppointemntsForBaseRenovation(RenovationPeriod renovationPeriod, int roomId);
    }
}
