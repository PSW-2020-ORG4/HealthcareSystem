using ScheduleService.DTO;
using ScheduleService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Services.AdvancedSearchStrategy
{
    public class TimeHasPriorityStrategy : IAdvancedSearchStrategy
    {
        private BasicSearchDTO SearchDTO { get; }
        private IEnumerable<string> PotentiallyAvailableDoctors { get; }

        public BasicSearchDTO GetSearchParameters()
        {
            throw new NotImplementedException();
        }
    }
}
