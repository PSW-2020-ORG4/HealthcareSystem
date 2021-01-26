using System;

namespace ScheduleService.Model
{
    public class Clock : IClock
    {
        public DateTime GetTimeLimit()
        {
            return Now() + new TimeSpan(48, 0, 0);
        }

        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
