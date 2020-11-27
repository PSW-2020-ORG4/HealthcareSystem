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
    public class MySqlConsumableEquipmentRepository : IConsumableEquipmentRepository
    {
        private readonly MyDbContext _context;
        public MySqlConsumableEquipmentRepository(MyDbContext context)
        {
            _context = context;
        }
        public bool DeleteEquipment(int id)
        {
            throw new NotImplementedException();
        }

        public List<ConsumableEquipment> GetAllEquipment()
        {
            return _context.ConsumableEquipments.ToList();
        }

        public ConsumableEquipment GetEquipment(int id)
        {
            return _context.ConsumableEquipments.SingleOrDefault(x => x.Id == id);
        }

        public ConsumableEquipment NewEquipment(ConsumableEquipment equipment)
        {
            _context.ConsumableEquipments.Add(equipment);
            _context.SaveChanges();
            return equipment;
        }

        public ConsumableEquipment SetEquipment(ConsumableEquipment equipment)
        {
            throw new NotImplementedException();
        }
    }
}
