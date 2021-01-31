using ScheduleService.DTO;

namespace ScheduleService.Services.AdvancedSearchStrategy
{
    public interface IAdvancedSearchStrategy
    {
        BasicSearchDTO GetSearchParameters();
    }
}
