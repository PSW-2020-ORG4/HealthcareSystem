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
    public class RenovationService : IRenovationService
    {
        private IRenovationRepository _renovationRepository;
        private IExaminationRepository _examinationRepository;
        private IExaminationService _examinationService;
        private IEquipmentTransferRepository _equipmentTransferRepository;

        public RenovationService(IRenovationRepository renovationRepository, IExaminationRepository examinationRepository, IExaminationService examinationService, IEquipmentTransferRepository equipmentTransferRepository)
        {
            _renovationRepository = renovationRepository;
            _examinationRepository = examinationRepository;
            _examinationService = examinationService;
            _equipmentTransferRepository = equipmentTransferRepository;
        }

        public BaseRenovation AddBaseRenovation(BaseRenovation baseRenovation)
        {
            if (_examinationService.GetExaminationsForPeriodAndRoom(baseRenovation.RenovationPeriod.BeginDate, baseRenovation.RenovationPeriod.EndDate, baseRenovation.RoomId).Count() > 0)
                return null;
            return _renovationRepository.AddRenovation(baseRenovation);
        }

        public void DeleteRenovation(int baseRenovationId)
        {
            _renovationRepository.DeleteRenovation(baseRenovationId);
        }

        public List<BaseRenovation> GetAllRenovations()
        {
            return _renovationRepository.GetAllRenovations();
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
                renovationPeriod.BeginDate = date.AddMinutes(30);
                renovationPeriod.EndDate = renovationPeriod.BeginDate.AddMinutes(timeSpanInMinutes);
                if (!CheckRoomAvailibility(date, roomId)) {
                    potentialDates = GetPotentiallyAlternativeAppointments(renovationPeriod.BeginDate);
                    i = 0;
                    continue;
                }
                if (i >= timeSpanInMinutes / 30 && CheckIfTimeValid(date.AddMinutes(30 - timeSpanInMinutes)) && CheckIfTimeValid(date.AddMinutes(30)))
                {
                    alternativeAppointments.Add(new RenovationPeriod(date.AddMinutes(30 - timeSpanInMinutes), date.AddMinutes(30)));
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
                || _equipmentTransferRepository.GetEquipmentTransferByRoomNumberAndDate(roomId, dateAndTimeOfTransfer) != null
                || _renovationRepository.GetAllRenovationsByRoomAndDate(roomId,dateAndTimeOfTransfer).Count > 0)
                return false;
            return true;
        }

        private List<DateTime> GetPotentiallyAlternativeAppointments(DateTime dateOfTransfer)
        {
            List<DateTime> potentiallyAlternativeAppointments = new List<DateTime>();
            DateTime timeFrom = dateOfTransfer;

            for (int i = 0; i < 1000; i++)
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

        public BaseRenovation GetRenovationById(int baseRenovationId)
        {
            return _renovationRepository.GetRenovationById(baseRenovationId);
        }

        public List<BaseRenovation> GetRenovationByRoomNumber(int roomNumber)
        {
            return _renovationRepository.GetAllRenovationsForTheRoom(roomNumber);
        }
        private DateTime SetNewDateTimesForRenovation(DateTime time)
        {
            int minutes = time.Minute % 30;
            int addition = 0;
            if (minutes != 0)
                addition = 30 - minutes;
            return time.AddMinutes(addition);
        }

        private Examination FindLastExamination(List<Examination> allExaminations) {
            Examination lastExamination = allExaminations[0];
            foreach (Examination e in allExaminations)
            {
                if (e.DateAndTime.CompareTo(lastExamination.DateAndTime) > 0)
                {
                    lastExamination = e;
                }
            }
            return lastExamination;
        }
        private Examination FindLastExaminationFromBothRooms(MergeRenovation renovation)
        {
            List<Examination> examinationsInFirstRoom = (List<Examination>)_examinationRepository.GetFollowingExaminationsByRoom(renovation.RoomId);
            List<Examination> examinationsInSecondRoom = (List<Examination>)_examinationRepository.GetFollowingExaminationsByRoom(renovation.SecondRoomId);
            return FindLastExamination(examinationsInFirstRoom).DateAndTime.CompareTo(FindLastExamination(examinationsInSecondRoom).DateAndTime) > 0 ? FindLastExamination(examinationsInFirstRoom) : FindLastExamination(examinationsInSecondRoom);

        }

        public MergeRenovation AddMergeRenovation(MergeRenovation renovation)
        {
            Examination lastExamination = FindLastExaminationFromBothRooms(renovation);
            if (lastExamination.DateAndTime.CompareTo(renovation.RenovationPeriod.BeginDate) >= 0)
                return null;
            _renovationRepository.AddRenovation(renovation);
            return renovation;          
        }
        public DivideRenovation AddDivideRenovation(DivideRenovation renovation)
        {
            List<Examination> examinationsInFirstRoom = (List<Examination>)_examinationRepository.GetFollowingExaminationsByRoom(renovation.RoomId);
            Examination lastExamination = FindLastExamination(examinationsInFirstRoom);
            if (lastExamination.DateAndTime.CompareTo(renovation.RenovationPeriod.BeginDate) >= 0)
                return null;
            _renovationRepository.AddRenovation(renovation);
            return renovation;
        }

        public List<RenovationPeriod> GetMergeRenovationAlternativeAppointmets(MergeRenovation renovation)
        {
            Examination lastExamination = FindLastExaminationFromBothRooms(renovation);
            return GetRenovationAlternativeAppointmets(renovation, lastExamination);
        }
        public List<RenovationPeriod> GetDivideRenovationAlternativeAppointmets(DivideRenovation renovation)
        {
            List<Examination> examinationsInFirstRoom = (List<Examination>)_examinationRepository.GetFollowingExaminationsByRoom(renovation.RoomId);
            Examination lastExamination = FindLastExamination(examinationsInFirstRoom);
            return GetRenovationAlternativeAppointmets(renovation, lastExamination);
        }

        private List<RenovationPeriod> GetRenovationAlternativeAppointmets(BaseRenovation renovation, Examination lastExamination)
        {
            int timeSpanInMinutes = (int)renovation.RenovationPeriod.EndDate.Subtract(renovation.RenovationPeriod.BeginDate).TotalMinutes;
            DateTime start = lastExamination.DateAndTime.AddMinutes(30);
            DateTime end = start.AddMinutes(timeSpanInMinutes);
            List<RenovationPeriod> alternativeAppointments = new List<RenovationPeriod>();
            int i = 0;
            while (i < 10)
            {
                if (CheckIfTimeValid(start) && CheckIfTimeValid(end))
                {
                    alternativeAppointments.Add(new RenovationPeriod(start, end));
                    i++;
                    start = end;
                    end = end.AddMinutes(timeSpanInMinutes);
                }
            }
            return alternativeAppointments;
        }
    }
}
