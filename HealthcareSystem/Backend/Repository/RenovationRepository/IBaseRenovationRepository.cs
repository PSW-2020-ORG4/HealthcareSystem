using Backend.Model.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Repository.RenovationRepository
{
    public interface IBaseRenovationRepository
    {
        BaseRenovation GetBaseRenovationById(int id);
        List<BaseRenovation> GetAllBaseRenovations();
        List<BaseRenovation> GetAllBaseRenovationsForTheRoom(int roomId);
        void DeleteBaseRenovation(int id);
        BaseRenovation AddBaseRenovation(BaseRenovation basicRenovation);

    }
}
