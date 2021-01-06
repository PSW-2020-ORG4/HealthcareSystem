﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Model
{
    public class Room
    {
        private int Id { get; }
        private RoomType RoomType { get; }
        private IEnumerable<Appointment> UnavailableAppointments { get; }

        public bool IsAvailable(Appointment appointment)
        {
            throw new NotImplementedException();
        }

    }
}
