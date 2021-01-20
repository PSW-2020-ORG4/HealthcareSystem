using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EventSourcingService.DTO;
using EventSourcingService.DTO.PatientSchedulingEventDTO;
using EventSourcingService.Model;
using EventSourcingService.Model.Enum;
using EventSourcingService.Repository;

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

            IEnumerable<PatientStepSchedulingEvent> closedScheduling = _patientSchedulingEventRepository.GetAll
                                                   (e => e.ClickEvent == Model.Enum.ClickEvent.Close);

            closedSchedulingStepStatistic.NumberOfClosuresOnDateStep = closedScheduling.Where(e => e.EventStep == Model.Enum.EventStep.Date).Count();
            closedSchedulingStepStatistic.NumberOfClosuresOnSpecialtyStep = closedScheduling.Where(e => e.EventStep == Model.Enum.EventStep.Specialty).Count();
            closedSchedulingStepStatistic.NumberOfClosuresOnDoctorStep = closedScheduling.Where(e => e.EventStep == Model.Enum.EventStep.Doctor).Count();
            closedSchedulingStepStatistic.NumberOfClosuresOnAppointmentStep = closedScheduling.Where(e => e.EventStep == Model.Enum.EventStep.Appointment).Count();
            closedSchedulingStepStatistic.TotalNumberOfClosures = closedScheduling.Count();

            EventStep mostClosedStep = EventStep.Date;
            int maxNumber = closedSchedulingStepStatistic.NumberOfClosuresOnDateStep;

            if (closedSchedulingStepStatistic.NumberOfClosuresOnSpecialtyStep > maxNumber)
            {
                mostClosedStep = EventStep.Specialty;
                maxNumber = closedSchedulingStepStatistic.NumberOfClosuresOnSpecialtyStep;
            }
            if(closedSchedulingStepStatistic.NumberOfClosuresOnDoctorStep > maxNumber)
            {
                mostClosedStep = EventStep.Doctor;
                maxNumber = closedSchedulingStepStatistic.NumberOfClosuresOnDoctorStep;
            }
            if(closedSchedulingStepStatistic.NumberOfClosuresOnAppointmentStep > maxNumber)
            {
                mostClosedStep = EventStep.Appointment;
                maxNumber = closedSchedulingStepStatistic.NumberOfClosuresOnAppointmentStep;
            }

            closedSchedulingStepStatistic.MostClosedStep = mostClosedStep;

            return closedSchedulingStepStatistic;
        }

        public StepPreviousStatisticDTO PreviousSchedulingStepStatistic()
        {
            StepPreviousStatisticDTO previousSchedulingStepStatistic = new StepPreviousStatisticDTO();

            IEnumerable<PatientStepSchedulingEvent> closedScheduling = _patientSchedulingEventRepository.GetAll
                                                   (e => e.ClickEvent == Model.Enum.ClickEvent.Previous);

            previousSchedulingStepStatistic.NumberOfPreviousOnSpecialtyStep = closedScheduling.Where(e => e.EventStep == Model.Enum.EventStep.Specialty).Count();
            previousSchedulingStepStatistic.NumberOfPrevoiusOnDoctorStep = closedScheduling.Where(e => e.EventStep == Model.Enum.EventStep.Doctor).Count();
            previousSchedulingStepStatistic.NumberOfPreviousOnAppointmentStep = closedScheduling.Where(e => e.EventStep == Model.Enum.EventStep.Appointment).Count();
            previousSchedulingStepStatistic.TotalNumberOfPrevious = closedScheduling.Count();

            EventStep mostReturnedStep = EventStep.Date;
            int maxNumber = previousSchedulingStepStatistic.NumberOfPreviousOnSpecialtyStep;

            if (previousSchedulingStepStatistic.NumberOfPrevoiusOnDoctorStep > maxNumber)
            {
                mostReturnedStep = EventStep.Specialty;
                maxNumber = previousSchedulingStepStatistic.NumberOfPrevoiusOnDoctorStep;
            }
            if (previousSchedulingStepStatistic.NumberOfPreviousOnAppointmentStep > maxNumber)
            {
                mostReturnedStep = EventStep.Doctor;
                maxNumber = previousSchedulingStepStatistic.NumberOfPreviousOnAppointmentStep;
            }
            previousSchedulingStepStatistic.MostReturnedStep = mostReturnedStep;

            return previousSchedulingStepStatistic;
        }

    }
}
