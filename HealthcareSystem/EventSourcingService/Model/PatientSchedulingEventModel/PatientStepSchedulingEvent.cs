﻿using EventSourcingService.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Model
{
    public class PatientStepSchedulingEvent : DomainEvent
    {
        public int StartSchedulingEventId { get; set; }
        public int UserAge { get; set; }
        public Gender UserGender { get; set; }
        public EventStep EventStep { get; set; }
        public ClickEvent ClickEvent { get; set; }

    }
}
