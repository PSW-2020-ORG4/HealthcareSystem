/***********************************************************************
 * Module:  ConsumableEquipmentService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.ConsumableEquipmentService
 ***********************************************************************/

using Backend.Model.DTO;
using Backend.Repository;
using Backend.Repository.EquipmentInRoomsRepository;
using Backend.Repository.ExaminationRepository;
using Backend.Repository.EquipmentTransferRepository;
using Backend.Service.RoomAndEquipment;
using Model.Manager;
using Model.PerformingExamination;
using System;
using System.Collections.Generic;
using Backend.Model.Manager;
using System.Linq;
using Backend.Repository.EquipmentInExaminationRepository;

namespace Service.RoomAndEquipment
{
    public class EquipmentService : IEquipmentService
    {
        private IEquipmentRepository _equipmentRepository;
        private IEquipmentInRoomsRepository _equipmentInRoomRepository;
        private readonly IExaminationRepository _examinationRepository;
        private readonly IEquipmentTransferRepository _equipmentTransferRepository;
        private readonly IEquipmentInExaminationRepository _equipmentInExaminationRepository;


        public EquipmentService(IEquipmentRepository equipmentRepository, IEquipmentInRoomsRepository equipmentInRoomRepository, IExaminationRepository examinationRepository, IEquipmentTransferRepository equipmentTransferRepository, IEquipmentInExaminationRepository equipmentInExaminationRepository)
        {
            _equipmentRepository = equipmentRepository;
            _equipmentInRoomRepository = equipmentInRoomRepository;
            _examinationRepository = examinationRepository;
            _equipmentTransferRepository = equipmentTransferRepository;
            _equipmentInExaminationRepository = equipmentInExaminationRepository;
        }

        public EquipmentService(IEquipmentRepository equipmentRepository, IEquipmentInRoomsRepository equipmentInRoomRepository, IExaminationRepository examinationRepository)
        {
            _equipmentRepository = equipmentRepository;
            _equipmentInRoomRepository = equipmentInRoomRepository;
            _examinationRepository = examinationRepository;

        }

        public Equipment AddEquipment(Equipment equipment)
        {
            return _equipmentRepository.AddEquipment(equipment);
        }
        public List<Equipment> GetEquipment()
        {
            return _equipmentRepository.GetAllEquipment();
        }

        public Model.Manager.Equipment EditEquipment(Model.Manager.Equipment equipment)
        {
            return _equipmentRepository.SetEquipment(equipment);
        }

        public void DeleteEquipment(int id)
        {
            _equipmentRepository.DeleteEquipment(id);
        }

        public List<Equipment> GetEquipmentWithRoomForSearchTerm(string searchTerm)
        {
            List<Equipment> equipment = GetEquipment();
            List<Equipment> validEquipment = new List<Equipment>();
            foreach (Equipment e in equipment)
            {
                if (CheckIfEquipmentNameContainsSearchTerm(e, searchTerm))
                    validEquipment.Add(e);
            }

            return validEquipment;
        }

        private bool CheckIfEquipmentNameContainsSearchTerm(Equipment equipment, string searchTerm)
        {
            if (equipment.Type.Name.ToString().ToLower().Contains(searchTerm.ToLower()))
                return true;
            else return false;
        }

        public Equipment GetEquipmentById(int equipmentId)
        {
            return _equipmentRepository.GetEquipmentById(equipmentId);
        }

        public List<Equipment> GetEquipmentByRoomNumber(int roomNumber)
        {
            List<EquipmentInRooms> equipmentInRooms = _equipmentInRoomRepository.GetEquipmentInRoomsByRoomNumber(roomNumber);
            List<Equipment> equipment = new List<Equipment>();
            foreach (var e in equipmentInRooms)
            {
                equipment.Add(GetEquipmentById(e.IdEquipment));
            }

            return equipment;
        }
        public int InitializeEquipmentTransfer(TransferEquipmentDTO transferEquipmentDTO)
        {
            int unavailaleRoomNumber = CheckStartingAndDestinationRoomsAvailability(transferEquipmentDTO);
            if (unavailaleRoomNumber != -1)
                return unavailaleRoomNumber;
            return CheckEquipmentAvailability(transferEquipmentDTO);
        }
        private int CheckStartingAndDestinationRoomsAvailability(TransferEquipmentDTO transferEquipmentDTO)
        {
            if (CheckRoomAvailibility(transferEquipmentDTO.DateAndTimeOfTransfer, transferEquipmentDTO.StartingRoomNumber) != -1)
                return transferEquipmentDTO.StartingRoomNumber;
            if (CheckRoomAvailibility(transferEquipmentDTO.DateAndTimeOfTransfer, transferEquipmentDTO.DestinationRoomNumber) != -1)
                return transferEquipmentDTO.DestinationRoomNumber;
            return -1;
        }

        private int CheckRoomAvailibility(DateTime dateAndTimeOfTransfer, int roomId)
        {
            if (_examinationRepository.GetExaminationsByRoomAndDateTime(roomId, dateAndTimeOfTransfer).Count > 0
                || _equipmentTransferRepository.GetEquipmentTransferByRoomNumberAndDate(roomId, dateAndTimeOfTransfer) != null)
                return roomId;
            return -1;
        }

        private int CheckEquipmentAvailability(TransferEquipmentDTO transferEquipmentDTO)
        {
            int roomNumber = -1;

            foreach (var e in _examinationRepository.GetFollowingExaminationsByRoom(transferEquipmentDTO.StartingRoomNumber).Where(e => e.DateAndTime.CompareTo(transferEquipmentDTO.DateAndTimeOfTransfer) > 0))
            {
                if (_equipmentInExaminationRepository.GetEquipmentInExaminationByExaminationId(e.Id).Find(eie => eie.EquipmentTypeID == transferEquipmentDTO.EquipmentTypeId) != null)
                    roomNumber = transferEquipmentDTO.StartingRoomNumber;
            }
            return roomNumber;
        }

        public void ScheduleEquipmentTrasfer(TransferEquipmentDTO transferEquipmentDTO)
        {
            _equipmentTransferRepository.AddEquipmentTransfer(new EquipmentTransfer(transferEquipmentDTO.StartingRoomNumber, transferEquipmentDTO.DateAndTimeOfTransfer));
            _equipmentTransferRepository.AddEquipmentTransfer(new EquipmentTransfer(transferEquipmentDTO.DestinationRoomNumber, transferEquipmentDTO.DateAndTimeOfTransfer));
        }

        public List<DateTime> GetAlternativeAppointments(TransferEquipmentDTO transferEquipmentDTO)
        {
            List<DateTime> alternativeAppointments = new List<DateTime>();

            if (CheckEquipmentAvailability(transferEquipmentDTO) != -1)
                transferEquipmentDTO.DateAndTimeOfTransfer = GetLastDateWhenEquipmentIsUsed(transferEquipmentDTO);
            do
                alternativeAppointments = ChooseAppointments(transferEquipmentDTO, alternativeAppointments);
            while (alternativeAppointments.Count != 10);

            return alternativeAppointments;    
        }
        private List<DateTime> ChooseAppointments(TransferEquipmentDTO transferEquipmentDTO, List<DateTime> alternativeAppointments)
        { 
            foreach (DateTime date in GetPotentiallyAlternativeAppointments(transferEquipmentDTO.DateAndTimeOfTransfer))
            {
                transferEquipmentDTO.DateAndTimeOfTransfer = date;

                if (CheckStartingAndDestinationRoomsAvailability(transferEquipmentDTO) == -1 
                    && _equipmentTransferRepository.GetEquipmentTransferByRoomNumberAndDate(transferEquipmentDTO.StartingRoomNumber,transferEquipmentDTO.DateAndTimeOfTransfer) == null
                    && _equipmentTransferRepository.GetEquipmentTransferByRoomNumberAndDate(transferEquipmentDTO.DestinationRoomNumber, transferEquipmentDTO.DateAndTimeOfTransfer) == null)
                    alternativeAppointments.Add(date);

                if (alternativeAppointments.Count == 10)
                    break;
            }
            return alternativeAppointments;
        }

        private List<DateTime> GetPotentiallyAlternativeAppointments(DateTime dateOfTransfer)
        {
            List<DateTime> potentiallyAlternativeAppointments = new List<DateTime>();
            DateTime timeFrom = dateOfTransfer;
          
            for (int i = 0; i < 10; i++)
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


        private DateTime GetLastDateWhenEquipmentIsUsed(TransferEquipmentDTO transferEquipmentDTO)
        {
            DateTime lastDateWhenEquipmentIsUsed = new DateTime();

            foreach (var e in _examinationRepository.GetFollowingExaminationsByRoom(transferEquipmentDTO.StartingRoomNumber).Where(e => e.DateAndTime.CompareTo(transferEquipmentDTO.DateAndTimeOfTransfer) > 0))
            {
                if (_equipmentInExaminationRepository.GetEquipmentInExaminationByExaminationId(e.Id).Find(eie => eie.EquipmentTypeID == transferEquipmentDTO.EquipmentTypeId) != null)
                    lastDateWhenEquipmentIsUsed = e.DateAndTime;
            }
            return lastDateWhenEquipmentIsUsed;
        }

    }
}