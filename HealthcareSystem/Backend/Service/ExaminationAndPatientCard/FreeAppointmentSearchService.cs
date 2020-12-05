using Backend.Model.DTO;
using Backend.Repository;
using Backend.Repository.ExaminationRepository;
using Backend.Repository.RoomRepository;
using Model.PerformingExamination;
using System;
using System.Collections.Generic;

namespace Backend.Service.ExaminationAndPatientCard
{
    public class FreeAppointmentSearchService : IFreeAppointmentSearchService
    {
        private TimeSpan _appointmentDuration;

        public FreeAppointmentSearchService(IDoctorRepository doctorRepository,
            IRoomRepository roomRepository, IExaminationRepository examinationRepository)
        {

        }

        public ICollection<Examination> BasicSearch(BasicAppointmentSearchDTO parameters)
        {
            /*
            for(Room in GenerateRooms)
                for(DateTime in GenerateStartTimes)
                    potentiallyAvailable.add(new Examination)

            for(Examination in potentiallyAvailable)
                if(IsAvailable(Examination))
                    actuallyAvailable.add                
            */

            throw new NotImplementedException();
        }

        public ICollection<Examination> SearchWithPriorities(AppointmentSearchWithPrioritiesDTO parameters)
        {
            /*call BasicSearch(parameters.InitialParameters)
             * if (emtpy)
             *      relax parameters and call BasicSearch again
             */
            throw new NotImplementedException();
        }

        private ICollection<DateTime> GenerateStartTimes(DateTime earliest, DateTime latest)
        {
            throw new NotImplementedException();
        }

        private Boolean IsAvailable(Examination examination)
        {
            throw new NotImplementedException();
        }
    }
}
