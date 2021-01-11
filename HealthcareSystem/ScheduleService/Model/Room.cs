using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Model
{
    public class Room
    {
        public int Id { get; }
        public RoomType RoomType { get; }
        private IEnumerable<Appointment> UnavailableAppointments { get; }

        public Room(int id, RoomType roomType)
        {
            Id = id;
            RoomType = roomType;
            UnavailableAppointments = new List<Appointment>();
        }

        public Room(int id, RoomType roomType, IEnumerable<DateTime> unavailableAppointments)
        {
            Id = id;
            RoomType = roomType;
            if (unavailableAppointments is null)
                UnavailableAppointments = new List<Appointment>();
            else
                UnavailableAppointments = unavailableAppointments.Select(a => new Appointment(a));
        }

        public bool IsAvailable(Appointment appointment)
        {
            return !UnavailableAppointments.Contains(appointment);
        }

    }
}
