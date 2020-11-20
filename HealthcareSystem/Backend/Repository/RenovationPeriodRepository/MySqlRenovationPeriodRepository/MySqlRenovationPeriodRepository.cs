using Backend.Model;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.RenovationPeriodRepository.MySqlRenovationPeriodRepository
{
    public class MySqlRenovationPeriodRepository : IRenovationPeriodRepository
    {
        private readonly MyDbContext _context;
        public MySqlRenovationPeriodRepository(MyDbContext context)
        {
            _context = context;
        }

        public void DeleteRenovationPeriod(int roomNumber)
        {
            RenovationPeriod renovationPeriod = GetRenovationPeriodByRoomNumber(roomNumber);
            _context.Remove(renovationPeriod);
            _context.SaveChanges();
        }

        public List<RenovationPeriod> GetAllRenovationPeriod()
        {
            return _context.RenovationPeriods.ToList();
        }

        public RenovationPeriod GetRenovationPeriodByRoomNumber(int roomNumber)
        {
            return _context.RenovationPeriods.Find(roomNumber);
        }

        public void AddRenovationPeriod(RenovationPeriod renovationPeriod)
        {
            _context.RenovationPeriods.Add(renovationPeriod);
            _context.SaveChanges();
        }

        public void UpdateRenovationPeriod(RenovationPeriod renovationPeriod)
        {
            _context.Update(renovationPeriod);
            _context.SaveChanges();
        }
    }
}
