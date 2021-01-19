using EventSourcingService.DTO;
using EventSourcingService.Model;
using EventSourcingService.Model.Enum;
using EventSourcingService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Service
{
    public class EventStorePatientEndSchedulingService : IEventStorePatientEndSchedulingService
    {
        private readonly IDomainEventRepository<PatientEndSchedulingEvent> _patientEndSchedulingEventRepository;

        public EventStorePatientEndSchedulingService(IDomainEventRepository<PatientEndSchedulingEvent> patientEndSchedulingEventRepository)
        {
            _patientEndSchedulingEventRepository = patientEndSchedulingEventRepository;
        }
        public PatientEndSchedulingEvent Add(PatientEndSchedulingEventDTO endSchedulingEventDTO)
        {
            return _patientEndSchedulingEventRepository.Add(new PatientEndSchedulingEvent()
            {
                StartSchedulingEventTime = endSchedulingEventDTO.StartSchedulingEventTime,
                UserAge = endSchedulingEventDTO.UserAge,
                UserGender = endSchedulingEventDTO.UserGender,
                ReasonForEndOfAppointment = endSchedulingEventDTO.ReasonForEndOfAppointment
            });
        }

        public IEnumerable<PatientEndSchedulingEvent> GetAll()
        {
            return _patientEndSchedulingEventRepository.GetAll();
        }

        public MinAvgMaxStatisticDTO SuccessfulSchedulingDuration()
        {
            MinAvgMaxStatisticDTO minAvgMaxStatisticDTO = new MinAvgMaxStatisticDTO();

            IEnumerable<TimeSpan> successfulSchedulingDuration = _patientEndSchedulingEventRepository.GetAll
                                  (e => e.ReasonForEndOfAppointment == ReasonForEndOfAppointment.Success).
                                  Select(e => e.TriggerTime - e.StartSchedulingEventTime);

            minAvgMaxStatisticDTO.Minimum = (int)successfulSchedulingDuration.Min(t => t.TotalMinutes);
            minAvgMaxStatisticDTO.Average = (int)successfulSchedulingDuration.Average(t => t.TotalMinutes);
            minAvgMaxStatisticDTO.Maximum = (int)successfulSchedulingDuration.Max(t => t.TotalMinutes);

            return minAvgMaxStatisticDTO;
        }

        public GenderStatisticDTO SuccessfulSchedulingGenderStatistic()
        {
            GenderStatisticDTO successfulSchedulingGenderStatistic = new GenderStatisticDTO();

            IEnumerable<PatientEndSchedulingEvent> successfulScheduling = _patientEndSchedulingEventRepository.GetAll
                                                   (e => e.ReasonForEndOfAppointment == ReasonForEndOfAppointment.Success);

            successfulSchedulingGenderStatistic.NumberOfWomen = successfulScheduling.Where(e => e.UserGender == Gender.Female).Count();
            successfulSchedulingGenderStatistic.NumberOfMen = successfulScheduling.Where(e => e.UserGender == Gender.Male).Count();
            successfulSchedulingGenderStatistic.TotalNumber = successfulScheduling.Count();


            return successfulSchedulingGenderStatistic;
        }

        public MinAvgMaxStatisticDTO SuccessfulSchedulingAgeStatistic()
        {
            MinAvgMaxStatisticDTO successfulSchedulingAgeStatistic = new MinAvgMaxStatisticDTO();

            IEnumerable<PatientEndSchedulingEvent> successfulScheduling = _patientEndSchedulingEventRepository.GetAll
                                                   (e => e.ReasonForEndOfAppointment == ReasonForEndOfAppointment.Success);

            successfulSchedulingAgeStatistic.Minimum = successfulScheduling.Min(e => e.UserAge);
            successfulSchedulingAgeStatistic.Average = (int)successfulScheduling.Average(e => e.UserAge);
            successfulSchedulingAgeStatistic.Maximum = successfulScheduling.Max(e => e.UserAge);

            return successfulSchedulingAgeStatistic;
        }
    }
}
