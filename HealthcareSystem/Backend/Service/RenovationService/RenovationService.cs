using Backend.Model.Enums;
using Backend.Model.Manager;
using Backend.Model.PerformingExamination;
using Backend.Repository.EquipmentInExaminationRepository;
using Backend.Repository.EquipmentTransferRepository;
using Backend.Repository.ExaminationRepository;
using Backend.Repository.RenovationRepository;
using Backend.Repository.RoomRepository;
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
        private IRoomRepository _roomRepository;

        public RenovationService(IRenovationRepository renovationRepository, IExaminationRepository examinationRepository, IExaminationService examinationService, IEquipmentTransferRepository equipmentTransferRepository, IRoomRepository roomRepository)
        {
            _renovationRepository = renovationRepository;
            _examinationRepository = examinationRepository;
            _examinationService = examinationService;
            _equipmentTransferRepository = equipmentTransferRepository;
            _roomRepository = roomRepository;
        }

        private bool CheckIfRoomIsAbsoulitelyAvailable(RenovationPeriod renovationPeriod, int roomId) {           
            DateTime renovationTime = renovationPeriod.BeginDate;
            while (renovationTime.CompareTo(renovationPeriod.EndDate) <= 0)
            {
                if (!CheckRoomAvailibility(renovationTime, roomId))                
                    return false;
                
                renovationTime = renovationTime.AddMinutes(30);
            }
            return true;
        }


        public BaseRenovation AddBaseRenovation(BaseRenovation baseRenovation)
        {

            if (!CheckIfRoomIsAbsoulitelyAvailable(baseRenovation.RenovationPeriod, baseRenovation.RoomId))
                return null;
            if (baseRenovation.TypeOfRenovation == TypeOfRenovation.MERGE_RENOVATION) {
                if (!CheckIfRoomIsAbsoulitelyAvailable(baseRenovation.RenovationPeriod, ((MergeRenovation)baseRenovation).SecondRoomId))
                    return null;
            
            }

            baseRenovation.RenovationPeriod.BeginDate = SetNewDateTimesForRenovation(baseRenovation.RenovationPeriod.BeginDate);
            baseRenovation.RenovationPeriod.EndDate = SetNewDateTimesForRenovation(baseRenovation.RenovationPeriod.EndDate);
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

        private DateTime FindLastAppointment(List<DateTime> endOfAppointments) {
            if (endOfAppointments.Count == 0)
                return DateTime.Now;
            DateTime lastAppointment = endOfAppointments[0];
            foreach (DateTime d in endOfAppointments.Where(x => x.CompareTo(lastAppointment) > 0))
                lastAppointment = d;

            return lastAppointment;
        }
        private DateTime FindLastAppointmentForSignleRoom(int roomId) {
            List<DateTime> appointments = new List<DateTime>();
            ((List<Examination>)_examinationRepository.GetFollowingExaminationsByRoom(roomId)).ForEach(x => appointments.Add(x.DateAndTime));
            ((List<EquipmentTransfer>)_equipmentTransferRepository.GetFollowingEquipmentTransversByRoom(roomId)).ForEach(x => appointments.Add(x.DateAndTimeOfTransfer));
            ((List<BaseRenovation>)_renovationRepository.GetFollowingRenovationsByRoom(roomId)).ForEach(x => appointments.Add(x.RenovationPeriod.EndDate));
            return FindLastAppointment(appointments);
        }
        private DateTime FindLastAppointmentForSecondRoom(int roomId)
        {
            List<DateTime> appointments = new List<DateTime>();
            ((List<Examination>)_examinationRepository.GetFollowingExaminationsByRoom(roomId)).ForEach(x => appointments.Add(x.DateAndTime));
            ((List<EquipmentTransfer>)_equipmentTransferRepository.GetFollowingEquipmentTransversByRoom(roomId)).ForEach(x => appointments.Add(x.DateAndTimeOfTransfer));
            //((List<BaseRenovation>)_renovationRepository.GetFollowingRenovationsBySecondRoom(roomId)).ForEach(x => appointments.Add(x.RenovationPeriod.EndDate));
            return FindLastAppointment(appointments);
        }

        private DateTime FindLastAppointmentFromBothRooms(MergeRenovation renovation)
        {
            DateTime appointmentInFirstRoom = FindLastAppointmentForSignleRoom(renovation.RoomId);
            DateTime appointmentInSecondRoom = FindLastAppointmentForSecondRoom(renovation.SecondRoomId);
            return appointmentInFirstRoom.CompareTo(appointmentInSecondRoom) > 0 ? appointmentInFirstRoom : appointmentInFirstRoom;

        }

        public MergeRenovation AddMergeRenovation(MergeRenovation renovation)
        {
            renovation.RenovationPeriod.BeginDate = SetNewDateTimesForRenovation(renovation.RenovationPeriod.BeginDate);
            renovation.RenovationPeriod.EndDate = SetNewDateTimesForRenovation(renovation.RenovationPeriod.EndDate);
            DateTime lastAppointment = FindLastAppointmentFromBothRooms(renovation);
            if (lastAppointment.CompareTo(renovation.RenovationPeriod.BeginDate) >= 0)
                return null;
            _equipmentTransferRepository.AddEquipmentTransfer( new EquipmentTransfer(renovation.RoomId,renovation.RenovationPeriod.BeginDate));
            _equipmentTransferRepository.AddEquipmentTransfer( new EquipmentTransfer(renovation.SecondRoomId,renovation.RenovationPeriod.BeginDate));
           _renovationRepository.AddRenovation(renovation);
            return renovation;          
        }

        public DivideRenovation AddDivideRenovation(DivideRenovation renovation)
        {
            renovation.RenovationPeriod.BeginDate = SetNewDateTimesForRenovation(renovation.RenovationPeriod.BeginDate);
            renovation.RenovationPeriod.EndDate = SetNewDateTimesForRenovation(renovation.RenovationPeriod.EndDate);
            DateTime lastAppointment = FindLastAppointmentForSignleRoom(renovation.RoomId);
            if (lastAppointment.CompareTo(renovation.RenovationPeriod.BeginDate) >= 0)
                return null;
            Room addedRoom = _roomRepository.AddRoom(new Room(renovation.SecondRoomType,0,0,true));
            _equipmentTransferRepository.AddEquipmentTransfer(new EquipmentTransfer(renovation.RoomId,renovation.RenovationPeriod.BeginDate));
            _equipmentTransferRepository.AddEquipmentTransfer(new EquipmentTransfer(addedRoom.Id,renovation.RenovationPeriod.BeginDate));
            _renovationRepository.AddRenovation(renovation);
            return renovation;
        }

        public List<RenovationPeriod> GetMergeRenovationAlternativeAppointmets(MergeRenovation renovation)
        {
            renovation.RenovationPeriod.BeginDate = SetNewDateTimesForRenovation(renovation.RenovationPeriod.BeginDate);
            renovation.RenovationPeriod.EndDate = SetNewDateTimesForRenovation(renovation.RenovationPeriod.EndDate);
            DateTime lastAppointment = FindLastAppointmentFromBothRooms(renovation);
            return GetRenovationAlternativeAppointmets(renovation, lastAppointment);
        }
        public List<RenovationPeriod> GetDivideRenovationAlternativeAppointmets(DivideRenovation renovation)
        {
            renovation.RenovationPeriod.BeginDate = SetNewDateTimesForRenovation(renovation.RenovationPeriod.BeginDate);
            renovation.RenovationPeriod.EndDate = SetNewDateTimesForRenovation(renovation.RenovationPeriod.EndDate);
            DateTime lastAppointment = FindLastAppointmentForSignleRoom(renovation.RoomId);
            return GetRenovationAlternativeAppointmets(renovation, lastAppointment);
        }

        private List<RenovationPeriod> GetRenovationAlternativeAppointmets(BaseRenovation renovation, DateTime lastAppointment)
        {
            int timeSpanInMinutes = (int)renovation.RenovationPeriod.EndDate.Subtract(renovation.RenovationPeriod.BeginDate).TotalMinutes;
            DateTime start = lastAppointment.AddMinutes(30);
            DateTime end = start.AddMinutes(timeSpanInMinutes);
            List<RenovationPeriod> alternativeAppointments = new List<RenovationPeriod>();
            int i = 0;
            while (i < 10)
            {
                if (CheckIfTimeValid(start) && CheckIfTimeValid(end))
                {
                    alternativeAppointments.Add(new RenovationPeriod(start, end));
                    i++;                    
                }
                start = end;
                end = end.AddMinutes(timeSpanInMinutes);
            }
            return alternativeAppointments;
        }

        public ICollection<BaseRenovation> GetRenovationForPeriodByRoomNumber(DateTime start, DateTime end, int roomNumber)
        {
            return _renovationRepository.GetRenovationForPeriodByRoomNumber(start, end, roomNumber);
        }
    }
}
