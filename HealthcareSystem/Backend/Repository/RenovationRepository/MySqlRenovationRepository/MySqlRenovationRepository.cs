using Backend.Model;
using Backend.Model.Enums;
using Backend.Model.Manager;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Repository.RenovationRepository.MySqlRenovationRepository
{
    public class MySqlRenovationRepository : IRenovationRepository
    {
        private readonly MyDbContext _context;
        public MySqlRenovationRepository(MyDbContext context)
        {
            _context = context;
        }
        public void DeleteRenovation(int id)
        {
            _context.BaseRenovation.Remove(_context.BaseRenovation.Find(id));
            _context.SaveChanges();
        }

        public List<BaseRenovation> GetAllRenovations()
        {
            return _context.BaseRenovation.ToList();
        }

        public BaseRenovation GetRenovationById(int id)
        {
            return _context.BaseRenovation.SingleOrDefault(x => x.Id == id);
        }

        public BaseRenovation AddRenovation(BaseRenovation baseRenovation)
        {           
            _context.BaseRenovation.Add(baseRenovation);
            _context.SaveChanges();
            return baseRenovation;
        }

        public List<BaseRenovation> GetAllRenovationsForTheRoom(int roomId)
        {
            var baseRenovationsForRoom = _context.BaseRenovation.Where(x => x.RoomId == roomId).ToList();
            return (List<BaseRenovation>) baseRenovationsForRoom;
        }
        public List<BaseRenovation> GetAllRenovationsByRoomAndDate(int roomId,DateTime date)
        {
            var baseRenovationsForRoom = _context.BaseRenovation.Where(x => x.RoomId == roomId && x.RenovationPeriod.BeginDate.CompareTo(date) <= 0 && x.RenovationPeriod.EndDate.CompareTo(date) >= 0).ToList();
            return (List<BaseRenovation>)baseRenovationsForRoom;
        }
    }
}
