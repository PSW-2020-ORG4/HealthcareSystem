using Backend.Model.DTO;
using Backend.Model.Exceptions;
using Backend.Repository;
using Backend.Repository.ExaminationRepository;
using Backend.Repository.RoomRepository;
using Model.Enums;
using Model.Manager;
using Model.PerformingExamination;
using Model.Users;
using Repository;
using System;
using System.Collections.Generic;

namespace Backend.Service.ExaminationAndPatientCard
{
    public class FreeAppointmentSearchService : IFreeAppointmentSearchService
    {
        private TimeSpan _appointmentDuration;
        private readonly IRoomRepository _roomRepository;
        private readonly IExaminationRepository _examinationRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IActivePatientCardRepository _activePatientCardRepository;

        public FreeAppointmentSearchService(IRoomRepository roomRepository, 
                                            IExaminationRepository examinationRepository,
                                            IDoctorRepository doctorRepository,
                                            IActivePatientCardRepository activePatientCardRepository)
        {
            _roomRepository = roomRepository;
            _examinationRepository = examinationRepository;
            _doctorRepository = doctorRepository;
            _activePatientCardRepository = activePatientCardRepository;
            _appointmentDuration = new TimeSpan(0,30,0);
        }

        public ICollection<Examination> BasicSearch(BasicAppointmentSearchDTO parameters)
        {
            ICollection<Examination> actuallyAvailableAppointments = new List<Examination>();

            foreach (Examination appointment in GetPotentiallyAvailableAppointments(parameters))
                if (IsAvailable(appointment)) 
                    actuallyAvailableAppointments.Add(appointment);

            return actuallyAvailableAppointments;
        }

        private ICollection<Examination> GetPotentiallyAvailableAppointments(BasicAppointmentSearchDTO parameters)
        {
            ICollection<Examination> potentiallyAvailableAppointments = new List<Examination>();

            foreach (Room room in GenerateRooms(TypeOfUsage.CONSULTING_ROOM, parameters.RequiredEquipmentTypes))
                foreach (DateTime dateTime in GenerateStartTimes(parameters.EarliestDateTime, parameters.LatestDateTime))
                    potentiallyAvailableAppointments.Add(new Examination(dateTime, parameters.DoctorJmbg, room.Id, parameters.PatientCardId));
     
            return potentiallyAvailableAppointments;
        }

        public ICollection<Examination> SearchWithPriorities(AppointmentSearchWithPrioritiesDTO parameters)
        {
            /*call BasicSearch(parameters.InitialParameters)
             * if (emtpy)
             *      relax parameters and call BasicSearch again
             */
            throw new NotImplementedException();
        }

        private ICollection<Room> GenerateRooms(TypeOfUsage typeOfUsage, ICollection<int> equipmentTypeIds)
        {
            return _roomRepository.GetRoomsByUsageAndEquipment(typeOfUsage, equipmentTypeIds);
        }

        private ICollection<DateTime> GenerateStartTimes(DateTime earliest, DateTime latest)
        {
            ICollection<DateTime> startTimes = new List<DateTime>();
            earliest = InitializeEarliestTime(earliest);
            latest = InitializeLatestTime(latest);

            for(DateTime time = earliest; DateTime.Compare(time, latest) < 0; time = time.Add(_appointmentDuration))
            {
                if (CheckIfTimeValid(time))
                {
                    startTimes.Add(time);
                    continue;
                }
                time = new DateTime(time.Year, time.Month, time.Day + 1, 6, 30, 0);
            }
                
            return startTimes;
        }
        private DateTime InitializeEarliestTime(DateTime earliest)
        {
            return new DateTime(earliest.Year,earliest.Month,earliest.Day,7,0,0);
        }

        private DateTime InitializeLatestTime(DateTime latest)
        {
            return new DateTime(latest.Year, latest.Month, latest.Day, 17, 0, 0);
        }
        private bool CheckIfTimeValid(DateTime dateTime)
        {
            if (TimeSpan.Compare(dateTime.TimeOfDay, new TimeSpan(7, 0, 0)) < 0)
                return false;
            if (TimeSpan.Compare(dateTime.TimeOfDay, new TimeSpan(17, 0, 0)) > 0)
                return false;
            return true;
        }
        private bool IsAvailable(Examination examination)
        {
            if (IsDoctorAvailable(examination.DoctorJmbg, examination.DateAndTime) &&
                IsRoomAvailable(examination.IdRoom, examination.DateAndTime) &&
                IsPatientAvailable(examination.IdPatientCard, examination.DateAndTime))
                return true;

            return false;
        }

        private bool IsDoctorAvailable(string doctorJmbg, DateTime dateTime)
        {
            if (!_doctorRepository.CheckIfDoctorExists(doctorJmbg))
                throw new BadRequestException("Doctor doesn't exist in database.");

            if (_examinationRepository.GetExaminationsByDoctorAndDateTime(doctorJmbg, dateTime).Count > 0)
                return false;

            return true;
        }

        private bool IsRoomAvailable(int roomId, DateTime dateTime)
        {
            if(!_roomRepository.CheckIfRoomExists(roomId))
                throw new BadRequestException("Room doesn't exist in database.");

            if (_examinationRepository.GetExaminationsByRoomAndDateTime(roomId, dateTime).Count > 0)
                return false;

            return true;
        }

        private bool IsPatientAvailable(int patientCardId, DateTime dateTime)
        {
            if(!_activePatientCardRepository.CheckIfPatientCardExists(patientCardId))
                throw new BadRequestException("Patient doesn't exist in database.");

            if (_examinationRepository.GetExaminationsByPatientAndDateTime(patientCardId, dateTime).Count > 0)
                return false;

            return true;
        }
    }
}
