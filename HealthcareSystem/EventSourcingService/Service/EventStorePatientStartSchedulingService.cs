﻿using EventSourcingService.DTO;
using EventSourcingService.Model;
using EventSourcingService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Service
{
    public class EventStorePatientStartSchedulingService : IEventStorePatientStartSchedulingService
    {
        private readonly IDomainEventRepository<PatientStartSchedulingEvent> _patientStartSchedulingEventRepository;

        public EventStorePatientStartSchedulingService(IDomainEventRepository<PatientStartSchedulingEvent> patientStartSchedulingEventRepository)
        {
            _patientStartSchedulingEventRepository = patientStartSchedulingEventRepository;
        }
        public void Add(PatientStartSchedulingEventDTO startSchedulingEventDTO)
        {
            _patientStartSchedulingEventRepository.Add(new PatientStartSchedulingEvent(startSchedulingEventDTO.TriggerTime));
        }

        public IEnumerable<PatientStartSchedulingEvent> GetAll()
        {
            return _patientStartSchedulingEventRepository.GetAll();
        }
    }
}
