using ScheduleService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Services.AdvancedSearchStrategy
{
    public class DoctorHasPriorityStrategy : IAdvancedSearchStrategy
    {
        private BasicSearchDTO SearchDTO { get; }

        public DoctorHasPriorityStrategy(BasicSearchDTO searchDTO)
        {
            SearchDTO = searchDTO;
        }

        public BasicSearchDTO GetSearchParameters()
        {
            throw new NotImplementedException();
        }
    }
}
