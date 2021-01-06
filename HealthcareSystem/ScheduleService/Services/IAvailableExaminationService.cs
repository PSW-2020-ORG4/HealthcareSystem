using ScheduleService.DTO;
using ScheduleService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Services
{
    public interface IAvailableExaminationService
    {
        IEnumerable<Examination> BasicSearch(BasicSearchDTO basicSearchDTO);
        IEnumerable<Examination> AdvancedSearch(AdvancedSearchDTO advancedSearchDTO);
    }
}
