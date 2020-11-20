using Backend.Model;
using Model.Enums;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _context.Rooms.Remove((Room)_context.Drugs.Where(d => d.Id == number));
            _context.SaveChanges();
        }

        public List<Room> GetAllRooms()
        {
            return _context.Rooms.ToList();
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
            return _context.Rooms.Where(d => d.Usage == usage).ToList();
        }

        public void AddRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public void UpdateRoom(Room room)
        {
            _context.Update(room);
            _context.SaveChanges();
        }
    }
}
