/***********************************************************************
 * Module:  RenovationPeriodService.cs
 * Author:  LukaRA252017
 * Purpose: Definition of the Class Service.RenovationPeriodService
 ***********************************************************************/


using Backend.Repository.ExaminationRepository;
using Backend.Repository.RenovationPeriodRepository;
using Backend.Service.PlacementInARoomAndRenovationPeriod;
using Model.Manager;
using Model.PerformingExamination;
using Repository;
using System.Collections.Generic;

namespace Service.PlacementInARoomAndRenovationPeriod
{
    public class RenovationPeriodService : IRenovationPeriodService
    {
        private IRenovationPeriodRepository _renovationPeriodRepository;
        private IExaminationRepository _scheduledExaminationRepository;
        public RenovationPeriodService(IRenovationPeriodRepository renovationPeriodRepository, IExaminationRepository scheduledExaminationRepository)
        {
            _renovationPeriodRepository = renovationPeriodRepository;
            _scheduledExaminationRepository = scheduledExaminationRepository;
        }

        //ovaj  ispod dodati
        private PlacementInSickRoomRepository placementInSickRoomRepository = new PlacementInSickRoomRepository();
        public void AddRenovationPeriod(Model.Manager.RenovationPeriod renovationPeriod)
        {
            List<Examination> examinations = _scheduledExaminationRepository.GetExaminationsByRoomAndDates(renovationPeriod.RoomNumber, renovationPeriod.BeginDate, renovationPeriod.EndDate);
            List<PlacemetnInARoom> placemetnInARooms = placementInSickRoomRepository.GetPlacementsByRoom(renovationPeriod.RoomNumber, renovationPeriod.BeginDate, renovationPeriod.EndDate);
            if (examinations.Count == 0 && placemetnInARooms.Count == 0)
                _renovationPeriodRepository.AddRenovationPeriod(renovationPeriod);
        }

        public void CancelRenovationPeriod(int roomNumber)
        {
            _renovationPeriodRepository.DeleteRenovationPeriod(roomNumber);
        }

        public List<RenovationPeriod> ViewRenovations()
        {
            return _renovationPeriodRepository.GetAllRenovationPeriod();
        }

        public Model.Manager.RenovationPeriod ViewRenovationByRoomNumber(int roomNumber)
        {
            return _renovationPeriodRepository.GetRenovationPeriodByRoomNumber(roomNumber);
        }

        public void UpdateRenovationPeriod(Model.Manager.RenovationPeriod renovationPeriod)
        {
            _renovationPeriodRepository.UpdateRenovationPeriod(renovationPeriod);
        }
    }
}
