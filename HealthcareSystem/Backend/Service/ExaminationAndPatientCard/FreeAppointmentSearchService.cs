﻿using Backend.Model.DTO;
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

        private readonly int PRIORITY_DATE_INTERVAL = 5;

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
            {
                if (IsAvailable(appointment))
                    actuallyAvailableAppointments.Add(appointment);
            }

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
                BasicAppointmentSearchDTO initialParameters = SetupDates(parameters.InitialParameters, i);

                ICollection<Examination> freeAppointments = BasicSearch(initialParameters);

                if (freeAppointments.Count != 0)
                    return freeAppointments;
            }

            return new List<Examination>();
        }

        private BasicAppointmentSearchDTO SetupDates(BasicAppointmentSearchDTO initialParameters, int datePeriod)
        {
            DateTime earliestDateTime = initialParameters.EarliestDateTime.AddDays(-datePeriod);
            DateTime fixedEarliestDateTime = GetFixedEarliestDateTime(earliestDateTime);
            DateTime latestDateTime = initialParameters.LatestDateTime.AddDays(datePeriod);
            initialParameters.EarliestDateTime = fixedEarliestDateTime;
            initialParameters.LatestDateTime = latestDateTime;

            return initialParameters;
        }

        private DateTime GetFixedEarliestDateTime(DateTime earliestDateTime)
        {
            if (CheckIfDatePassed(earliestDateTime))
                return earliestDateTime.AddDays(1);
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
