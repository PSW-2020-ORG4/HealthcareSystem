using Backend.Model.DTO;
using Backend.Repository.ExaminationRepository;
using Backend.Repository.RoomRepository;
using Model.Enums;
using Model.Manager;
using Model.PerformingExamination;
using System;
using System.Collections.Generic;

namespace Backend.Service.ExaminationAndPatientCard
{
    public class FreeAppointmentSearchService : IFreeAppointmentSearchService
    {
        private TimeSpan _appointmentDuration;
        private readonly IRoomRepository _roomRepository;
        private readonly IExaminationRepository _examinationRepository;

        public FreeAppointmentSearchService(IRoomRepository roomRepository, IExaminationRepository examinationRepository)
        {
            _roomRepository = roomRepository;
            _examinationRepository = examinationRepository;
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

        public ICollection<Examination> GetPotentiallyAvailableAppointments(BasicAppointmentSearchDTO parameters)
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

            for(DateTime time = earliest; DateTime.Compare(time, latest) <= 0; time = time.Add(_appointmentDuration))
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
            return _examinationRepository.IsDoctorAvailable(doctorJmbg, dateTime);
        }

        private bool IsRoomAvailable(int roomId, DateTime dateTime)
        {
            return _examinationRepository.IsRoomAvailable(roomId,dateTime);
        }

        private bool IsPatientAvailable(int patientCardId, DateTime dateTime)
        {
            return _examinationRepository.IsPatientAvailable(patientCardId,dateTime);
        }
    }
}
