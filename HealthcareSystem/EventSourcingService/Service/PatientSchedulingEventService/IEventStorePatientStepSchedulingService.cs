﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EventSourcingService.DTO;
using EventSourcingService.DTO.PatientSchedulingEventDTO;
using EventSourcingService.Model;

namespace EventSourcingService.Service
{
    public interface IEventStorePatientStepSchedulingService
    {
        IEnumerable<PatientStepSchedulingEvent> GetAll();
        PatientStepSchedulingEvent Add(PatientStepSchedulingEventDTO schedulingEventDTO);
        StepClosureStatisticDTO ClosedSchedulingStepStatistic();
        StepPreviousStatisticDTO PreviousSchedulingStepStatistic();
        bool Contain(int id);
        public SchedulingStepsStatisticDTO SchedulingStepsStatistic();
    }
}