using ScheduleService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Repository
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetByEquipmentTypes(IEnumerable<int> equipmentTypeIds, DateTime startDate, DateTime endDate);
    }
}
