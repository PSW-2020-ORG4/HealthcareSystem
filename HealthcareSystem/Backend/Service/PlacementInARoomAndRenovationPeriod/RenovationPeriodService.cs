/***********************************************************************
 * Module:  RenovationPeriodService.cs
 * Author:  LukaRA252017
 * Purpose: Definition of the Class Service.RenovationPeriodService
 ***********************************************************************/


using Backend.Repository.ExaminationRepository.FileExaminationRepository;
using Controller.PlacementInARoomAndRenovationPeriod;
using Model.Manager;
using Model.PerformingExamination;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.PlacementInARoomAndRenovationPeriod
{
    public class RenovationPeriodService
    {
        private RenovationPeriodRepository renovationPeriodRepository = new RenovationPeriodRepository();
        private FileScheduledExaminationRepository scheduledExaminationRepository = new FileScheduledExaminationRepository();
        private PlacementInSickRoomRepository placementInSickRoomRepository = new PlacementInSickRoomRepository();
        public Model.Manager.RenovationPeriod ScheduleRenovation(Model.Manager.RenovationPeriod renovationPeriod)
        {
            List <Examination>  examinations = scheduledExaminationRepository.GetExaminationsByRoomAndDates(renovationPeriod.room.Number, renovationPeriod.BeginDate, renovationPeriod.EndDate);
            List<PlacemetnInARoom> placemetnInARooms = placementInSickRoomRepository.GetPlacementsByRoom(renovationPeriod.room.Number,renovationPeriod.BeginDate,renovationPeriod.EndDate);
            if (examinations.Count == 0 && placemetnInARooms.Count == 0)
                return renovationPeriodRepository.NewRenovationPeriod(renovationPeriod);
            return null;
        }

        public bool CancelRenovation(int roomNumber)
        {
            return renovationPeriodRepository.DeleteRenovationPeriod(roomNumber);
        }

        public List<RenovationPeriod> ViewRenovations()
        {
            return renovationPeriodRepository.GetAllRenovationPeriod();
        }

        public Model.Manager.RenovationPeriod ViewRenovationByRoomNumber(int roomNumber)
        {
            return renovationPeriodRepository.GetRenovationPeriodByRoomNumber(roomNumber);
        }

        public Model.Manager.RenovationPeriod EditRenovation(Model.Manager.RenovationPeriod renovationPeriod)
        {
            return renovationPeriodRepository.SetRenovationPeriod(renovationPeriod);
        }
    }
}
