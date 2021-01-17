using Backend.Model;
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
        public void DeleteBaseRenovation(int id)
        {
            _context.BaseRenovation.Remove(_context.BaseRenovation.Find(id));
            _context.SaveChanges();
        }

        public List<BaseRenovation> GetAllBaseRenovations()
        {
            return _context.BaseRenovation.ToList();
        }

        public BaseRenovation GetBaseRenovationById(int id)
        {
            return _context.BaseRenovation.SingleOrDefault(x => x.Id == id);
        }

        public BaseRenovation AddBaseRenovation(BaseRenovation baseRenovation)
        {           
            _context.BaseRenovation.Add(baseRenovation);
            _context.SaveChanges();
            return baseRenovation;
        }

        public List<BaseRenovation> GetAllBaseRenovationsForTheRoom(int roomId)
        {
            var baseRenovationsForRoom = _context.BaseRenovation.Where(x => x.RoomId == roomId).ToList();
            return (List<BaseRenovation>) baseRenovationsForRoom;
        }       
    }
}
