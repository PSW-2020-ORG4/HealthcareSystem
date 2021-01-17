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
        DivideRenovation AddDivideRenovation(DivideRenovation renovation);
        List<BaseRenovation> GetAllRenovations();
        BaseRenovation GetRenovationById(int baseRenovationId);
        void DeleteRenovation(int baseRenovationId);
        List<BaseRenovation> GetRenovationByRoomNumber(int roomNumber);
        List<RenovationPeriod> GetAlternativeAppointemntsForBaseRenovation(RenovationPeriod renovationPeriod, int roomId);
        List<RenovationPeriod> GetMergeRenovationAlternativeAppointmets(MergeRenovation renovation);
        List<RenovationPeriod> GetDivideRenovationAlternativeAppointmets(DivideRenovation renovation);


    }
}
