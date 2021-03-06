﻿using Backend.Model.Manager;
using System;
using System.Collections.Generic;

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
        ICollection<BaseRenovation> GetFollowingRenovationsByRoom(int roomId);
        ICollection<MergeRenovation> GetFollowingRenovationsBySecondRoom(int roomId);
        ICollection<BaseRenovation> GetRenovationForPeriodByRoomNumber(DateTime start, DateTime end, int roomNumber);
    }
}
