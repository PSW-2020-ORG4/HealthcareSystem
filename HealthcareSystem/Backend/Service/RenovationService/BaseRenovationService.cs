using Backend.Model.Manager;
using Backend.Model.PerformingExamination;
using Backend.Repository.EquipmentInExaminationRepository;
using Backend.Repository.EquipmentTransferRepository;
using Backend.Repository.ExaminationRepository;
using Backend.Repository.RenovationRepository;
using Backend.Service.ExaminationAndPatientCard;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Service.RenovationService
{
    public class BaseRenovationService : IBaseRenovationService
    {
        private IBaseRenovationRepository _baseRenovationRepository;
        private IExaminationRepository _examinationRepository;
        private IExaminationService _examinationService;
        private IEquipmentTransferRepository _equipmentTransferRepository;

        public BaseRenovationService(IBaseRenovationRepository baseRenovationRepository, IExaminationRepository examinationRepository,IExaminationService examinationService, IEquipmentTransferRepository equipmentTransferRepository)
        {
            _baseRenovationRepository = baseRenovationRepository;
            _examinationRepository = examinationRepository;
            _examinationService = examinationService;
            _equipmentTransferRepository = equipmentTransferRepository;
        }

        public BaseRenovation AddBaseRenovation(BaseRenovation baseRenovation)
        {
            if (_examinationService.GetExaminationsForPeriodAndRoom(baseRenovation.RenovationPeriod.BeginDate, baseRenovation.RenovationPeriod.EndDate, baseRenovation.RoomId).Count() > 0)
                return null;
            return _baseRenovationRepository.AddBaseRenovation(baseRenovation);
        }

        public void DeleteBaseRenovation(int baseRenovationId)
        {
            _baseRenovationRepository.DeleteBaseRenovation(baseRenovationId);
        }

        public List<BaseRenovation> GetAllBaseRenovations()
        {
            return _baseRenovationRepository.GetAllBaseRenovations();  
        }

      
        private List<RenovationPeriod> ChooseAppointments(RenovationPeriod renovationPeriod, int roomId)
        {
            List<RenovationPeriod> alternativeAppointments = new List<RenovationPeriod>();
            List<DateTime> potentialDates = GetPotentiallyAlternativeAppointments(renovationPeriod.BeginDate);
            int timeSpanInMinutes = (int)renovationPeriod.EndDate.Subtract(renovationPeriod.BeginDate).TotalMinutes;
            int i = 0;
            foreach (DateTime date in potentialDates)
            {
                i++;
                if (!CheckRoomAvailibility(date, roomId)) {
                    renovationPeriod.BeginDate = date;
                    renovationPeriod.BeginDate = date.AddMinutes(30);
                    renovationPeriod.EndDate = renovationPeriod.BeginDate.AddMinutes(timeSpanInMinutes);
                    potentialDates = GetPotentiallyAlternativeAppointments(renovationPeriod.BeginDate);
                    i = 0;
                    continue;
                }
                if (i >= timeSpanInMinutes / 30) 
                {
                    alternativeAppointments.Add(renovationPeriod);
                    i = 0;
                }

                if (alternativeAppointments.Count == 10)
                    break;
            }
            return alternativeAppointments;
        }

        private bool CheckRoomAvailibility(DateTime dateAndTimeOfTransfer, int roomId)
        {
            if (_examinationRepository.GetExaminationsByRoomAndDateTime(roomId, dateAndTimeOfTransfer).Count > 0
                || _equipmentTransferRepository.GetEquipmentTransferByRoomNumberAndDate(roomId, dateAndTimeOfTransfer) != null)
                return false;
            return true;
        }

        private List<DateTime> GetPotentiallyAlternativeAppointments(DateTime dateOfTransfer)
        {
            List<DateTime> potentiallyAlternativeAppointments = new List<DateTime>();
            DateTime timeFrom = dateOfTransfer;

            for (int i = 0; i < 100; i++)
            {
                if (CheckIfTimeValid(timeFrom))
                {
                    potentiallyAlternativeAppointments.Add(timeFrom);
                    timeFrom = timeFrom.AddMinutes(30);
                    continue;
                }
                timeFrom = new DateTime(timeFrom.Year, timeFrom.Month, timeFrom.Day, 7, 0, 0);
                timeFrom = timeFrom.AddDays(1);
            }
            return potentiallyAlternativeAppointments;
        }

        private bool CheckIfTimeValid(DateTime dateTime)
        {
            if (TimeSpan.Compare(dateTime.TimeOfDay, new TimeSpan(7, 0, 0)) < 0)
                return false;
            if (TimeSpan.Compare(dateTime.TimeOfDay, new TimeSpan(17, 0, 0)) >= 0)
                return false;
            return true;
        }

        public List<RenovationPeriod> GetAlternativeAppointemntsForBaseRenovation(RenovationPeriod renovationPeriod, int roomId)
        {
            renovationPeriod.BeginDate = SetNewDateTimesForRenovation(renovationPeriod.BeginDate);
            renovationPeriod.EndDate = SetNewDateTimesForRenovation(renovationPeriod.EndDate);
            List<RenovationPeriod> alternativeAppointments = new List<RenovationPeriod>();

            do
                alternativeAppointments = ChooseAppointments(renovationPeriod, roomId);
                
            while (alternativeAppointments.Count != 10);

            return alternativeAppointments;
        }

        public BaseRenovation GetBaseRenovationById(int baseRenovationId)
        {
            return _baseRenovationRepository.GetBaseRenovationById(baseRenovationId);
        }

        public List<BaseRenovation> GetBaseRenovationByRoomNumber(int roomNumber)
        {
            return _baseRenovationRepository.GetAllBaseRenovationsForTheRoom(roomNumber);
        }
        private DateTime SetNewDateTimesForRenovation(DateTime time)
        {
            int minutes = time.Minute % 30;
            int addition = 0;
            if (minutes != 0)
                addition = 30 - minutes;
            return time.AddMinutes(addition);
        }
    }
}
