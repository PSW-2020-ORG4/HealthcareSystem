using EventSourcingService.DTO;
using EventSourcingService.Model;
using EventSourcingService.Model.Enum;
using EventSourcingService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

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

            try
            {
                IEnumerable<TimeSpan> successfulSchedulingDuration = _patientEndSchedulingEventRepository.GetAll
                                  (e => e.ReasonForEndOfAppointment == ReasonForEndOfAppointment.Success).
                                  Select(e => e.TriggerTime - e.StartSchedulingEventTime);

                minAvgMaxStatisticDTO.Minimum = (int)successfulSchedulingDuration.Min(t => t.TotalMinutes);
                minAvgMaxStatisticDTO.Average = (int)successfulSchedulingDuration.Average(t => t.TotalMinutes);
                minAvgMaxStatisticDTO.Maximum = (int)successfulSchedulingDuration.Max(t => t.TotalMinutes);
            }
            catch (Exception)
            {
                new MinAvgMaxStatisticDTO();
            }

            return minAvgMaxStatisticDTO;
        }

        public GenderStatisticDTO SuccessfulSchedulingGenderStatistic()
        {
            GenderStatisticDTO successfulSchedulingGenderStatistic = new GenderStatisticDTO();

            try
            {
                IEnumerable<PatientEndSchedulingEvent> successfulScheduling = _patientEndSchedulingEventRepository.GetAll
                                                   (e => e.ReasonForEndOfAppointment == ReasonForEndOfAppointment.Success);

                successfulSchedulingGenderStatistic.NumberOfWomen = successfulScheduling.Where(e => e.UserGender == Gender.Female).Count();
                successfulSchedulingGenderStatistic.NumberOfMen = successfulScheduling.Where(e => e.UserGender == Gender.Male).Count();
                successfulSchedulingGenderStatistic.TotalNumber = successfulScheduling.Count();
            }
            catch (Exception)
            {
                return new GenderStatisticDTO();
            }

            return successfulSchedulingGenderStatistic;
        }

        public MinAvgMaxStatisticDTO SuccessfulSchedulingAgeStatistic()
        {
            MinAvgMaxStatisticDTO successfulSchedulingAgeStatistic = new MinAvgMaxStatisticDTO();

            try
            {
                IEnumerable<PatientEndSchedulingEvent> successfulScheduling = _patientEndSchedulingEventRepository.GetAll
                                                   (e => e.ReasonForEndOfAppointment == ReasonForEndOfAppointment.Success);

                successfulSchedulingAgeStatistic.Minimum = successfulScheduling.Min(e => e.UserAge);
                successfulSchedulingAgeStatistic.Average = (int)successfulScheduling.Average(e => e.UserAge);
                successfulSchedulingAgeStatistic.Maximum = successfulScheduling.Max(e => e.UserAge);
            }
            catch (Exception)
            {
                return new MinAvgMaxStatisticDTO();
            }

            return successfulSchedulingAgeStatistic;
        }

        public AverageDurationDTO SuccessfulSchedulingAgeDuration()
        {
            AverageDurationDTO averageDTO = new AverageDurationDTO();

            try
            {
                int averageAge = (int)_patientEndSchedulingEventRepository.GetAll().Average(e => e.UserAge);

                IEnumerable<PatientEndSchedulingEvent> successfulScheduling = _patientEndSchedulingEventRepository.GetAll
                                                       (e => e.ReasonForEndOfAppointment == ReasonForEndOfAppointment.Success);

                IEnumerable<TimeSpan> youthDuration = successfulScheduling.Where(e => e.UserAge < averageAge)
                                                      .Select(e => e.TriggerTime - e.StartSchedulingEventTime);
                IEnumerable<TimeSpan> elderDuration = successfulScheduling.Where(e => e.UserAge >= averageAge)
                                                      .Select(e => e.TriggerTime - e.StartSchedulingEventTime);

                averageDTO.DurationFirst = (int)youthDuration.DefaultIfEmpty().Average(t => t.TotalMinutes);
                averageDTO.DurationSecond = (int)elderDuration.DefaultIfEmpty().Average(t => t.TotalMinutes);
            }
            catch (Exception)
            {
                return new AverageDurationDTO();
            }

            return averageDTO;
        }

        public AverageDurationDTO SuccessfulSchedulingGenderDuration()
        {
            AverageDurationDTO averageDTO = new AverageDurationDTO();

            try
            {
                IEnumerable<PatientEndSchedulingEvent> successfulScheduling = _patientEndSchedulingEventRepository.GetAll
                                                   (e => e.ReasonForEndOfAppointment == ReasonForEndOfAppointment.Success);

                IEnumerable<TimeSpan> womenDuration = successfulScheduling.Where(e => e.UserGender == Gender.Female)
                                                      .Select(e => e.TriggerTime - e.StartSchedulingEventTime);
                IEnumerable<TimeSpan> menDuration = successfulScheduling.Where(e => e.UserGender == Gender.Male)
                                                      .Select(e => e.TriggerTime - e.StartSchedulingEventTime);

                averageDTO.DurationFirst = (int)womenDuration.DefaultIfEmpty().Average(t => t.TotalMinutes);
                averageDTO.DurationSecond = (int)menDuration.DefaultIfEmpty().Average(t => t.TotalMinutes);
            }
            catch (Exception)
            {
                return new AverageDurationDTO();
            }

            return averageDTO;
        }
        public AverageDurationDTO UnsuccessfulSchedulingAgeDuration()
        {
            AverageDurationDTO averageDTO = new AverageDurationDTO();

            try
            {
                int averageAge = (int)_patientEndSchedulingEventRepository.GetAll().Average(e => e.UserAge);

                IEnumerable<PatientEndSchedulingEvent> unsuccessfulScheduling = _patientEndSchedulingEventRepository.GetAll
                                                       (e => e.ReasonForEndOfAppointment == ReasonForEndOfAppointment.Unsuccess);

                IEnumerable<TimeSpan> youthDuration = unsuccessfulScheduling.Where(e => e.UserAge < averageAge)
                                                      .Select(e => e.TriggerTime - e.StartSchedulingEventTime);
                IEnumerable<TimeSpan> elderDuration = unsuccessfulScheduling.Where(e => e.UserAge >= averageAge)
                                                      .Select(e => e.TriggerTime - e.StartSchedulingEventTime);

                averageDTO.DurationFirst = (int)youthDuration.DefaultIfEmpty().Average(t => t.TotalMinutes);
                averageDTO.DurationSecond = (int)elderDuration.DefaultIfEmpty().Average(t => t.TotalMinutes);
            }
            catch (Exception)
            {
                return new AverageDurationDTO();
            }

            return averageDTO;
        }
        public AverageDurationDTO UnsuccessfulSchedulingGenderDuration()
        {
            AverageDurationDTO averageDTO = new AverageDurationDTO();

            try
            {
                IEnumerable<PatientEndSchedulingEvent> unsuccessfulScheduling = _patientEndSchedulingEventRepository.GetAll
                                                   (e => e.ReasonForEndOfAppointment == ReasonForEndOfAppointment.Unsuccess);

                IEnumerable<TimeSpan> womenDuration = unsuccessfulScheduling.Where(e => e.UserGender == Gender.Female)
                                                      .Select(e => e.TriggerTime - e.StartSchedulingEventTime);
                IEnumerable<TimeSpan> menDuration = unsuccessfulScheduling.Where(e => e.UserGender == Gender.Male)
                                                      .Select(e => e.TriggerTime - e.StartSchedulingEventTime);

                averageDTO.DurationFirst = (int)womenDuration.DefaultIfEmpty().Average(t => t.TotalMinutes);
                averageDTO.DurationSecond = (int)menDuration.DefaultIfEmpty().Average(t => t.TotalMinutes);
            }
            catch (Exception)
            {
                return new AverageDurationDTO();
            }
            
            return averageDTO;
        }

        public bool Contain(int id)
        {
            return _patientEndSchedulingEventRepository.GetAll().Where(e => e.Id == id).Any();
        }

        public SuccessfulAndUnsuccessfulSchedulingDTO SuccessfulAndUnsuccessfulScheduling()
        {
            SuccessfulAndUnsuccessfulSchedulingDTO schedulingDTO = new SuccessfulAndUnsuccessfulSchedulingDTO();

            schedulingDTO.NumberOfSuccessfulScheduling = _patientEndSchedulingEventRepository.GetAll
                                                   (e => e.ReasonForEndOfAppointment == ReasonForEndOfAppointment.Success).Count();
            schedulingDTO.NumberOfUnsuccessfulScheduling = _patientEndSchedulingEventRepository.GetAll
                                                   (e => e.ReasonForEndOfAppointment == ReasonForEndOfAppointment.Unsuccess).Count();
            schedulingDTO.NumberOfScheduling = _patientEndSchedulingEventRepository.GetAll().Count();

            return schedulingDTO;
        }
    }
}
