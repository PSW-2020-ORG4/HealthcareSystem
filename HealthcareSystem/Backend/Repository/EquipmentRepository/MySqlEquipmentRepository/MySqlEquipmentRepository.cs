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
    public class MySqlEquipmentRepository : IEquipmentRepository
    {
        private readonly MyDbContext _context;
        public MySqlEquipmentRepository(MyDbContext context)
        {
            _context = context;
        }
        public bool DeleteEquipment(int id)
        {
            throw new NotImplementedException();
        }

        public List<Equipment> GetAllEquipment()
        {
            return _context.Equipment.ToList();
        }

        public Equipment GetEquipment(int id)
        {
            return _context.Equipment.SingleOrDefault(x => x.Id == id);
        }

        public Equipment AddEquipment(Equipment equipment)
        {
            equipment.Type = _context.EquipmentTypes.SingleOrDefault(x => x.Id == equipment.Type.Id);
            _context.Equipment.Add(equipment);
            _context.SaveChanges();
            return equipment;
        }

        public Equipment SetEquipment(Equipment equipment)
        {
            throw new NotImplementedException();
        }
    }
}
