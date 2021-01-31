using EventSourcingService.DTO;
using EventSourcingService.DTO.PatientSchedulingEventDTO;
using EventSourcingService.Model;
using EventSourcingService.Model.Enum;
using EventSourcingService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingService.Service
{
    public class EventStorePatientStepSchedulingService : IEventStorePatientStepSchedulingService
    {

        private readonly IDomainEventRepository<PatientStepSchedulingEvent> _patientSchedulingEventRepository;

        public EventStorePatientStepSchedulingService(IDomainEventRepository<PatientStepSchedulingEvent> patientSchedulingEventRepository)
        {
            _patientSchedulingEventRepository = patientSchedulingEventRepository;
        }

        public IEnumerable<PatientStepSchedulingEvent> GetAll()
        {
            return _patientSchedulingEventRepository.GetAll();
        }

        public PatientStepSchedulingEvent Add(PatientStepSchedulingEventDTO patientStepSchedulingEventDTO)
        {
            return _patientSchedulingEventRepository.Add(new PatientStepSchedulingEvent()
            {
                StartSchedulingEventId = patientStepSchedulingEventDTO.StartSchedulingEventId,
                UserAge = patientStepSchedulingEventDTO.UserAge,
                UserGender = patientStepSchedulingEventDTO.UserGender,
                EventStep = patientStepSchedulingEventDTO.EventStep,
                ClickEvent = patientStepSchedulingEventDTO.ClickEvent
            });
        }

        public StepClosureStatisticDTO ClosedSchedulingStepStatistic()
        {
            StepClosureStatisticDTO closedSchedulingStepStatistic = new StepClosureStatisticDTO();

            try
            {
                IEnumerable<PatientStepSchedulingEvent> closedScheduling = _patientSchedulingEventRepository.GetAll
                                                   (e => e.ClickEvent == ClickEvent.Close);

                closedSchedulingStepStatistic.NumberOfClosuresOnDateStep = closedScheduling.Where(e => e.EventStep == EventStep.Date).Count();
                closedSchedulingStepStatistic.NumberOfClosuresOnSpecialtyStep = closedScheduling.Where(e => e.EventStep == EventStep.Specialty).Count();
                closedSchedulingStepStatistic.NumberOfClosuresOnDoctorStep = closedScheduling.Where(e => e.EventStep == EventStep.Doctor).Count();
                closedSchedulingStepStatistic.NumberOfClosuresOnAppointmentStep = closedScheduling.Where(e => e.EventStep == EventStep.Appointment).Count();
                closedSchedulingStepStatistic.TotalNumberOfClosures = closedScheduling.Count();

                EventStep mostClosedStep = EventStep.Date;
                int maxNumber = closedSchedulingStepStatistic.NumberOfClosuresOnDateStep;

                if (closedSchedulingStepStatistic.NumberOfClosuresOnSpecialtyStep > maxNumber)
                {
                    mostClosedStep = EventStep.Specialty;
                    maxNumber = closedSchedulingStepStatistic.NumberOfClosuresOnSpecialtyStep;
                }
                if (closedSchedulingStepStatistic.NumberOfClosuresOnDoctorStep > maxNumber)
                {
                    mostClosedStep = EventStep.Doctor;
                    maxNumber = closedSchedulingStepStatistic.NumberOfClosuresOnDoctorStep;
                }
                if (closedSchedulingStepStatistic.NumberOfClosuresOnAppointmentStep > maxNumber)
                {
                    mostClosedStep = EventStep.Appointment;
                    maxNumber = closedSchedulingStepStatistic.NumberOfClosuresOnAppointmentStep;
                }

                closedSchedulingStepStatistic.MostClosedStep = mostClosedStep;
            }
            catch (Exception)
            {
                return new StepClosureStatisticDTO();
            }

            return closedSchedulingStepStatistic;
        }

        public StepPreviousStatisticDTO PreviousSchedulingStepStatistic()
        {
            StepPreviousStatisticDTO previousSchedulingStepStatistic = new StepPreviousStatisticDTO();

            try
            {
                IEnumerable<PatientStepSchedulingEvent> closedScheduling = _patientSchedulingEventRepository.GetAll
                                                   (e => e.ClickEvent == ClickEvent.Previous);

                previousSchedulingStepStatistic.NumberOfPreviousOnSpecialtyStep = closedScheduling.Where(e => e.EventStep == EventStep.Specialty).Count();
                previousSchedulingStepStatistic.NumberOfPrevoiusOnDoctorStep = closedScheduling.Where(e => e.EventStep == EventStep.Doctor).Count();
                previousSchedulingStepStatistic.NumberOfPreviousOnAppointmentStep = closedScheduling.Where(e => e.EventStep == EventStep.Appointment).Count();
                previousSchedulingStepStatistic.TotalNumberOfPrevious = closedScheduling.Count();

                EventStep mostReturnedStep = EventStep.Specialty;
                int maxNumber = previousSchedulingStepStatistic.NumberOfPreviousOnSpecialtyStep;

                if (previousSchedulingStepStatistic.NumberOfPrevoiusOnDoctorStep > maxNumber)
                {
                    mostReturnedStep = EventStep.Doctor;
                    maxNumber = previousSchedulingStepStatistic.NumberOfPrevoiusOnDoctorStep;
                }
                if (previousSchedulingStepStatistic.NumberOfPreviousOnAppointmentStep > maxNumber)
                {
                    mostReturnedStep = EventStep.Appointment;
                    maxNumber = previousSchedulingStepStatistic.NumberOfPreviousOnAppointmentStep;
                }
                previousSchedulingStepStatistic.MostReturnedStep = mostReturnedStep;
            }
            catch (Exception)
            {
                return new StepPreviousStatisticDTO();
            }

            return previousSchedulingStepStatistic;
        }

        public bool Contain(int id)
        {
            return _patientSchedulingEventRepository.GetAll().Where(e => e.Id == id).Any();
        }

        public SchedulingStepsStatisticDTO SchedulingStepsStatistic()
        {
            SchedulingStepsStatisticDTO schedulingStepsStatisticDTO = new SchedulingStepsStatisticDTO();

            schedulingStepsStatisticDTO.NumberOfClosedScheduling = _patientSchedulingEventRepository.GetAll(e => e.ClickEvent == ClickEvent.Close).Count();
            schedulingStepsStatisticDTO.NumberOfNextSteps = _patientSchedulingEventRepository.GetAll(e => e.ClickEvent == ClickEvent.Next).Count();
            schedulingStepsStatisticDTO.NumberOfPreviousSteps = _patientSchedulingEventRepository.GetAll(e => e.ClickEvent == ClickEvent.Previous).Count();
            schedulingStepsStatisticDTO.NumberOfSteps = _patientSchedulingEventRepository.GetAll().Count();

            return schedulingStepsStatisticDTO;
        }

    }
}
