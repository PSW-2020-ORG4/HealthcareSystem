using ScheduleService.Model;
using System;
using System.Collections.Generic;

namespace ScheduleService.Repository
{
    public interface IRoomRepository
    {
        Room Get(int id);
        Room Get(int id, DateTime startDate, DateTime endDate);
        IEnumerable<Room> GetByEquipmentTypes(RoomType type, IEnumerable<int> equipmentTypeIds, DateTime startDate, DateTime endDate);
    }
}
