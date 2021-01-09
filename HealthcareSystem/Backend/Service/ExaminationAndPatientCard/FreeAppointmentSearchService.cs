using Backend.Model.DTO;
using Backend.Model.Enums;
using Backend.Model.Exceptions;
using Backend.Model.PerformingExamination;
using Backend.Repository;
using Backend.Repository.ExaminationRepository;
using Backend.Repository.RoomRepository;
using Backend.Service.RoomAndEquipment;
using Model.Manager;
using Model.Users;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Service.ExaminationAndPatientCard
{
    public class FreeAppointmentSearchService : IFreeAppointmentSearchService
    {
        private TimeSpan _appointmentDuration;
        private readonly IRoomService _roomService;
        private readonly IExaminationRepository _examinationRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IActivePatientCardRepository _activePatientCardRepository;
        private readonly IEquipmentInExaminationService _equipmentInExaminationService;

        private readonly int PRIORITY_DATE_INTERVAL = 5;

        public FreeAppointmentSearchService(IRoomService roomService, 
                                            IExaminationRepository examinationRepository,
                                            IDoctorRepository doctorRepository,
                                            IActivePatientCardRepository activePatientCardRepository,
                                            IEquipmentInExaminationService equipmentInExaminationService)
        {
            _roomService = roomService;
            _examinationRepository = examinationRepository;
            _doctorRepository = doctorRepository;
            _activePatientCardRepository = activePatientCardRepository;
            _appointmentDuration = new TimeSpan(0,30,0);
            _equipmentInExaminationService = equipmentInExaminationService;
        }

        public ICollection<Examination> BasicSearch(BasicAppointmentSearchDTO parameters)
        {
            ICollection<Examination> actuallyAvailableAppointments = new List<Examination>();

            foreach (Examination appointment in GetPotentiallyAvailableAppointments(parameters))
            {
                if (IsAvailable(appointment))
                    actuallyAvailableAppointments.Add(appointment);
            }

            return actuallyAvailableAppointments;
        }

        public ICollection<Examination> GetUnchangedAppointmentsForEmergency(AppointmentSearchWithPrioritiesDTO parameters)
        {
            parameters.InitialParameters.EarliestDateTime = GetNewStartTime(parameters.InitialParameters.EarliestDateTime);
            parameters.InitialParameters.LatestDateTime = parameters.InitialParameters.EarliestDateTime.AddHours(2);

            ICollection<Doctor> allDoctors = _doctorRepository.GetDoctorsBySpecialty(parameters.SpecialtyId);
            List<Examination> allUnchangedAppointments = new List<Examination>();
            foreach (Doctor doctor in allDoctors)
            {
                parameters.InitialParameters.DoctorJmbg = doctor.Jmbg;
                allUnchangedAppointments.AddRange(BasicSearch(parameters.InitialParameters));
            }
            allUnchangedAppointments.ForEach(e => e.ExaminationStatus = ExaminationStatus.AVAILABLE);  

            return allUnchangedAppointments;
        }

        public ICollection<Examination> GetShiftedAndSortedAppoinmentsForEmergency(AppointmentSearchWithPrioritiesDTO parameters)
        {
            parameters.InitialParameters.EarliestDateTime = GetNewStartTime(parameters.InitialParameters.EarliestDateTime);
            parameters.InitialParameters.LatestDateTime = parameters.InitialParameters.EarliestDateTime.AddHours(2);

            List<Examination> unavailableAppointments = GetUnavailableAppointments(parameters.InitialParameters);
            List<Examination> adequateAppoinments = GetOnlyAdequateAppointments(unavailableAppointments, parameters);

            ICollection<Examination> shiftedAppointments = new List<Examination>();
            foreach (Examination examination in unavailableAppointments)
            {
                List<Examination> availableAppointments = GetShiftedAppointmentForEmergency(parameters.InitialParameters, examination);
                availableAppointments = availableAppointments.OrderBy(e => e.DateAndTime).ToList();
                foreach (Examination appointment in availableAppointments)
                {
                    if (IsShiftedAppoinmentAvailable(appointment, shiftedAppointments))
                    {
                        shiftedAppointments.Add(appointment);
                        break;
                    }
                }
            }

            return shiftedAppointments;
        }

        private bool IsShiftedAppoinmentAvailable(Examination appointment, ICollection<Examination> shiftedAppointments)
        {
            foreach(Examination shiftedAppointment in shiftedAppointments)
            {
                if (shiftedAppointment.DateAndTime.Equals(appointment.DateAndTime)
                    && (shiftedAppointment.IdPatientCard == appointment.IdPatientCard
                        || shiftedAppointment.DoctorJmbg == appointment.DoctorJmbg))
                    return false;
            }
            return true;
        }

        private List<Examination> GetOnlyAdequateAppointments(List<Examination> unavailableAppointments, AppointmentSearchWithPrioritiesDTO parameters)
        {
            foreach (Examination examination in unavailableAppointments.ToList())
            {
                if (!_roomService.CheckIfRoomHasRequiredEquipment(examination.IdRoom, parameters.InitialParameters.RequiredEquipmentTypes)
                    || !IsPatientAvailable(parameters.InitialParameters.PatientCardId, examination.DateAndTime)
                    || !examination.Doctor.CheckIfDoctorHasSpecialty(parameters.SpecialtyId))
                    unavailableAppointments.Remove(examination);
            }
            return unavailableAppointments;
        }

        private List<Examination> GetUnavailableAppointments(BasicAppointmentSearchDTO parameters)
        {
            return (List<Examination>) _examinationRepository.GetExaminationsForPeriod(parameters.EarliestDateTime, parameters.LatestDateTime);
        }

        private List<Examination> GetShiftedAppointmentForEmergency(BasicAppointmentSearchDTO parameters, Examination examination)
        {
            DateTime startDateTime = parameters.LatestDateTime;
            DateTime endDateTime = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, 17, 0, 0);
            List<int> requiredEquipmentTypes = GetRequiredEquipmentForExamination(examination);            

            for (int i = 1; i <= 13; i++)
            {
                parameters = new BasicAppointmentSearchDTO(
                    examination.IdPatientCard,
                    examination.DoctorJmbg,
                    requiredEquipmentTypes,
                    startDateTime,
                    endDateTime
                );

                List<Examination> potentialAppointments = (List<Examination>)BasicSearch(parameters);
                if (potentialAppointments.Count != 0)
                    return potentialAppointments;

                startDateTime = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, 7, 0, 0);
                startDateTime = startDateTime.AddDays(1);
                endDateTime = endDateTime.AddDays(1);
            }
            return null;
        }

        private List<int> GetRequiredEquipmentForExamination(Examination examination)
        {
            List<int> requiredEquipmentTypes = new List<int>();
            foreach (EquipmentInExamination e in _equipmentInExaminationService.GetEquipmentInExaminationFromExaminationID(examination.Id))
                requiredEquipmentTypes.Add(e.EquipmentTypeID);
            return requiredEquipmentTypes;
        }

        private DateTime GetNewStartTime(DateTime startTime)
        {
            int minutes = startTime.Minute % _appointmentDuration.Minutes;
            if (minutes == 0)
                return startTime;
            int addition = _appointmentDuration.Minutes - minutes;
            return startTime.AddMinutes(addition);
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
            ICollection<Doctor> allDoctors = _doctorRepository.GetDoctorsBySpecialty(parameters.SpecialtyId);
            List<Examination> freeAppointments = new List<Examination>();
            foreach(Doctor doctor in allDoctors)
            {
                parameters.InitialParameters.DoctorJmbg = doctor.Jmbg;
                freeAppointments.AddRange(BasicSearch(parameters.InitialParameters));                 
            }

            return freeAppointments;
        }

        private ICollection<Examination> RelaxDates(AppointmentSearchWithPrioritiesDTO parameters)
        {
            for (int i = 0; i < PRIORITY_DATE_INTERVAL; i++)
            {
                BasicAppointmentSearchDTO initialParameters = SetupDates(parameters.InitialParameters);

                ICollection<Examination> freeAppointments = BasicSearch(initialParameters);

                if (freeAppointments.Count != 0)
                    return freeAppointments;
            }

            return new List<Examination>();
        }

        private BasicAppointmentSearchDTO SetupDates(BasicAppointmentSearchDTO initialParameters)
        {
            DateTime earliestDateTime = initialParameters.EarliestDateTime.AddDays(-1);
            DateTime fixedEarliestDateTime = GetFixedEarliestDateTime(earliestDateTime);
            DateTime latestDateTime = initialParameters.LatestDateTime.AddDays(1);
            initialParameters.EarliestDateTime = fixedEarliestDateTime;
            initialParameters.LatestDateTime = latestDateTime;

            return initialParameters;
        }

        private DateTime GetFixedEarliestDateTime(DateTime earliestDateTime)
        {
            if (CheckIfDatePassed(earliestDateTime))
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 7, 0, 0, DateTimeKind.Utc);
            return earliestDateTime;
        }

        private bool CheckIfDatePassed(DateTime earliestDateTime)
        {
            return earliestDateTime.CompareTo(DateTime.Now) == -1 ? true : false;
        }

        private ICollection<Room> GenerateRooms(TypeOfUsage typeOfUsage, ICollection<int> equipmentTypeIds)
        {
            return _roomService.GetRoomsByUsageAndEquipment(typeOfUsage, equipmentTypeIds);
        }

        private ICollection<DateTime> GenerateStartTimes(DateTime earliest, DateTime latest)
        {
            ICollection<DateTime> startTimes = new List<DateTime>();

            for(DateTime time = earliest; DateTime.Compare(time, latest) < 0; time = time.Add(_appointmentDuration))
            {
                if (CheckIfTimeValid(time))
                {
                    startTimes.Add(time);
                    continue;
                }
                time = new DateTime(time.Year, time.Month, time.Day, 6, 30, 0);
                time = time.AddDays(1);
            }
                
            return startTimes;
        }

        private bool CheckIfTimeValid(DateTime dateTime)
        {
            if (TimeSpan.Compare(dateTime.TimeOfDay, new TimeSpan(7, 0, 0)) < 0)
                return false;
            if (TimeSpan.Compare(dateTime.TimeOfDay, new TimeSpan(17, 0, 0)) >= 0)
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
