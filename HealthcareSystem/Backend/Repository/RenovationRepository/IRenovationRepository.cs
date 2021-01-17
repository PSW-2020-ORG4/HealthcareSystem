using Backend.Model.Manager;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Repository.RenovationRepository
{
    public interface IRenovationRepository
    {
        BaseRenovation GetRenovationById(int id);
        List<BaseRenovation> GetAllRenovations();
        List<BaseRenovation> GetAllRenovationsForTheRoom(int roomId);
        void DeleteRenovation(int id);
        BaseRenovation AddRenovation(BaseRenovation basicRenovation);
        List<BaseRenovation> GetAllRenovationsByRoomAndDate(int roomId, DateTime date);

    }
}
