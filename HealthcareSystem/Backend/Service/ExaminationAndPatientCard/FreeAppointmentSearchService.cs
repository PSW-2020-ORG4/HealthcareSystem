using Backend.Model.DTO;
using Backend.Model.Enums;
using Backend.Model.Exceptions;
using Backend.Repository;
using Backend.Repository.ExaminationRepository;
using Backend.Repository.RoomRepository;
using Backend.Service.RoomAndEquipment;
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
        private readonly IRoomService _roomService;
        private readonly IExaminationRepository _examinationRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IActivePatientCardRepository _activePatientCardRepository;

        public FreeAppointmentSearchService(IRoomService roomService, 
                                            IExaminationRepository examinationRepository,
                                            IDoctorRepository doctorRepository,
                                            IActivePatientCardRepository activePatientCardRepository)
        {
            _roomService = roomService;
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
            ICollection<Examination> freeAppointments = BasicSearch(parameters.InitialParameters);
            if (freeAppointments.Count == 0)
            {               
                if (parameters.Priority == SearchPriority.Doctor)
                    return RelaxDates(parameters);
                else return RelaxDoctor(parameters);
            }

            return freeAppointments;
        }

        private ICollection<Examination> RelaxDoctor(AppointmentSearchWithPrioritiesDTO parameters)
        {
            ICollection<Doctor> allDoctors = _doctorRepository.GetAllDoctors();
            List<Examination> freeAppointments = new List<Examination>();
            foreach(Doctor doctor in allDoctors)
            {
                if(doctor.CheckIfDoctorHasSpecialty(parameters.SpecialtyId))
                    freeAppointments.AddRange(BasicSearch(parameters.InitialParameters));
            }

            return freeAppointments;
        }

        private ICollection<Examination> RelaxDates(AppointmentSearchWithPrioritiesDTO parameters)
        {
            DateTime earliestDateTime = parameters.InitialParameters.EarliestDateTime.AddDays(-3);
            DateTime latestDateTime = parameters.InitialParameters.LatestDateTime.AddDays(3);
            parameters.InitialParameters.EarliestDateTime = earliestDateTime;
            parameters.InitialParameters.LatestDateTime = latestDateTime;

            return BasicSearch(parameters.InitialParameters);
        }

        private ICollection<Room> GenerateRooms(TypeOfUsage typeOfUsage, ICollection<int> equipmentTypeIds)
        {
            return _roomService.GetRoomsByUsageAndEquipment(typeOfUsage, equipmentTypeIds);
        }

        private ICollection<DateTime> GenerateStartTimes(DateTime earliest, DateTime latest)
        {
            ICollection<DateTime> startTimes = new List<DateTime>();

            for(DateTime time = earliest; DateTime.Compare(time, latest) < 0; time = time.Add(_appointmentDuration))
                startTimes.Add(time);
   
            return startTimes;
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
            if(!_roomService.CheckIfRoomExists(roomId))
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
