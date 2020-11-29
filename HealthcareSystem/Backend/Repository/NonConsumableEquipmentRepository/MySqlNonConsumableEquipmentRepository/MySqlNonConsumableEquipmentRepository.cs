using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;
using Backend.Repository;
using Model.Manager;

namespace Backend.Repository
{
   public class MySqlNonConsumableEquipmentRepository : INonConsumableEquipmentRepository
    {
        private readonly MyDbContext _context;

        public MySqlNonConsumableEquipmentRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool DeleteEquipment(int id)
        {
            throw new NotImplementedException();
        }

        public List<NonConsumableEquipment> GetAllEquipment()
        {
            return _context.NonConsumableEquipments.ToList();
        }

        public NonConsumableEquipment GetEquipment(int id)
        {
            return _context.NonConsumableEquipments.SingleOrDefault(x => x.Id == id);
        }

        public NonConsumableEquipment NewEquipment(NonConsumableEquipment equipment)
        {
            _context.Add(equipment);
            _context.SaveChanges();
            return equipment;
        }

        public NonConsumableEquipment SetEquipment(NonConsumableEquipment equipment)
        {
            throw new NotImplementedException();
        }
    }
}
