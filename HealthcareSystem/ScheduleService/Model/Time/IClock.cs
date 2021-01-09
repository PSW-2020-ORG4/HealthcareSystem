using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Model
{
    public interface IClock
    {
        DateTime Now();
        DateTime GetTimeLimit();
    }
}
