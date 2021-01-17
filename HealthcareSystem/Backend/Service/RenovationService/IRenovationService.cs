using Backend.Model.Manager;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Service.RenovationService
{
    public interface IRenovationService
    {
        BaseRenovation AddBaseRenovation(BaseRenovation baseRenovation);
        MergeRenovation AddMergeRenovation(MergeRenovation mergeRenovation);
        List<BaseRenovation> GetAllBaseRenovations();
        BaseRenovation GetBaseRenovationById(int baseRenovationId);
        void DeleteBaseRenovation(int baseRenovationId);
        List<BaseRenovation> GetBaseRenovationByRoomNumber(int roomNumber);
        List<RenovationPeriod> GetAlternativeAppointemntsForBaseRenovation(RenovationPeriod renovationPeriod, int roomId);
    }
}
