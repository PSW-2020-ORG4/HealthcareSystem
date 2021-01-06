using ScheduleService.DTO;
using ScheduleService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Services
{
    public class AvailableExaminationService : IAvailableExaminationService
    {
        public IEnumerable<Examination> AdvancedSearch(AdvancedSearchDTO advancedSearchDTO)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Examination> BasicSearch(BasicSearchDTO basicSearchDTO)
        {
            throw new NotImplementedException();
        }
    }
}
