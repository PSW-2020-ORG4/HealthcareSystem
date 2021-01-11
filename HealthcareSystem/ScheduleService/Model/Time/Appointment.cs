using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Model
{
    public class Appointment : IComparable<Appointment>
    {
        public DateTime Value { get; }

        public Appointment(DateTime value)
        {
            Value = Normalize(value);
        }

        private DateTime Normalize(DateTime value)
        {
            TimeSpan timeSpan = new TimeSpan(0, 30, 0);
            long ticks = (value.Ticks + timeSpan.Ticks - 1) / timeSpan.Ticks;
            return new DateTime(ticks * timeSpan.Ticks);
        }

        public Appointment NextAppointment()
        {
            return new Appointment(Value.AddMinutes(30));
        }

        public override bool Equals(object obj)
        {
            return obj is Appointment appointment &&
                   Value == appointment.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public int CompareTo(Appointment other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}
