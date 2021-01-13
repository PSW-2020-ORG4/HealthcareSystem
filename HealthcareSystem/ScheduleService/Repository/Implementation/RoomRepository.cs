using Backend.Model;
using Backend.Service.RoomAndEquipment;
using ScheduleService.CustomException;
using ScheduleService.Model;
using ScheduleService.Repository.BackendMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Repository.Implementation
{
    public class RoomRepository : IRoomRepository
    {
        private MyDbContext _context;
        private Backend.Service.RoomAndEquipment.IRoomService _roomService;

        public RoomRepository(MyDbContext context, IRoomService roomService)
        {
            _context = context;
            _roomService = roomService;
        }

        public Room Get(int id)
        {
            try
            {
                var room = _context.Rooms.Find(id);
                if (room is null)
                    throw new NotFoundException("Room with id " + id + " not found.");
                return new Room(id, room.Usage.ToRoomType());
            }
            catch (ScheduleServiceException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }

        public Room Get(int id, DateTime startDate, DateTime endDate)
        {
            try
            {
                var room = _context.Rooms.Find(id);
                if (room is null)
                    throw new NotFoundException("Room with id " + id + " not found.");
                var unavailable = GetUnavailableAppointments(id, startDate, endDate);
                return new Room(id, room.Usage.ToRoomType(), unavailable);
            }
            catch (ScheduleServiceException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }

        public IEnumerable<Room> GetByEquipmentTypes(RoomType type, IEnumerable<int> equipmentTypeIds, DateTime startDate, DateTime endDate)
        {
            try
            {
                var rooms = _roomService.GetRoomsByUsageAndEquipment(type.ToBackendRoomType(), equipmentTypeIds.ToList());
                return rooms.Select(r => new Room(
                    r.Id, r.Usage.ToRoomType(), GetUnavailableAppointments(r.Id, startDate, endDate)));
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }

        private IEnumerable<DateTime> GetUnavailableAppointments(int id, DateTime start, DateTime end)
        {
            try
            {
                return _context.Examinations.Where(
                    e => e.IdRoom.Equals(id)
                    && e.DateAndTime <= end
                    && e.DateAndTime >= start
                    && e.ExaminationStatus != Backend.Model.Enums.ExaminationStatus.CANCELED).Select(
                    e => e.DateAndTime);
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }
    }
}
