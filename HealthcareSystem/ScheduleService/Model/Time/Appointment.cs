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
            return value;
        }

        public Appointment NextAppointment()
        {
            throw new NotImplementedException();
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
