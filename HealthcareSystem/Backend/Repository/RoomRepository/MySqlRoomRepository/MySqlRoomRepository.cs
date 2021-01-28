using Backend.Model;
using Backend.Model.Enums;
using Model.Manager;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository.RoomRepository.MySqlRoomRepository
{
    public class MySqlRoomRepository : IRoomRepository
    {
        private readonly MyDbContext _context;

        public MySqlRoomRepository(MyDbContext context)
        {
            _context = context;
        }

        public void DeleteRoom(int number)
        {
            Room room = GetRoomByNumber(number);
            _context.Remove(room);
            _context.SaveChanges();
        }

        public List<Room> GetAllRooms()
        {
            return _context.Rooms.Where(d => !d.Renovation).ToList();
        }

        public int getLastId()
        {
            //TODO:Implement
            return 0;
        }

        public Room GetRoomByNumber(int number)
        {
            return _context.Rooms.Find(number);
        }

        public List<Room> GetRoomsByUsage(TypeOfUsage usage)
        {
            return _context.Rooms.Where(d => d.Usage == usage && !d.Renovation).ToList();
        }

        public Room AddRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
            return (Room)_context.Rooms.SingleOrDefault(r => r.Id == room.Id);
        }

        public void UpdateRoom(Room room)
        {
            _context.Update(room);
            _context.SaveChanges();
        }

        public bool CheckIfRoomExists(int roomId)
        {
            if (_context.Rooms.Find(roomId) == null)
                return false;
            if (_context.Rooms.Find(roomId).Renovation)
                return false;
            return true;
        }
    }
}
