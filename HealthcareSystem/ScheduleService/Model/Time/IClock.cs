using System;

namespace ScheduleService.Model
{
    public interface IClock
    {
        DateTime Now();
        DateTime GetTimeLimit();
    }
}
