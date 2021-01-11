using ScheduleService.DTO;
using ScheduleService.Model;
using System.Collections.Generic;

namespace ScheduleService.Services
{
    public interface IAvailableExaminationService
    {
        IEnumerable<Examination> BasicSearch(BasicSearchDTO basicSearchDTO);
        IEnumerable<Examination> AdvancedSearch(AdvancedSearchDTO advancedSearchDTO);
    }
}
