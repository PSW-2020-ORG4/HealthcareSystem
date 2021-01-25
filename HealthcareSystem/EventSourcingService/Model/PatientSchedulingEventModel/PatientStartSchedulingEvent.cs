﻿using EventSourcingService.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Model
{
    public class PatientStartSchedulingEvent : DomainEvent
    {
        public int UserAge { get; set; }
        public Gender UserGender { get; set; }
    }
}