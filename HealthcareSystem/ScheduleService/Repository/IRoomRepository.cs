using ScheduleService.Model;
using System;
using System.Collections.Generic;

namespace ScheduleService.Repository
{
    public interface IRoomRepository
    {
        Room Get(int id);
        IEnumerable<Room> GetByEquipmentTypes(IEnumerable<int> equipmentTypeIds, DateTime startDate, DateTime endDate);
    }
}
