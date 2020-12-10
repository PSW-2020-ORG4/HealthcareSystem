using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;
using Backend.Model.Manager;
using Backend.Repository;
using Model.Manager;

namespace Backend.Repository
{
    public class MySqlEquipmentTypeRepository : IEquipmentTypeRepository
    {
        private readonly MyDbContext _context;
        public MySqlEquipmentTypeRepository(MyDbContext context)
        {
            _context = context;
        }  
        
        public EquipmentType GetEquipmentType(int id)
        {
            return _context.EquipmentTypes.SingleOrDefault(x => x.Id == id);
        }

        public List<EquipmentType> GetAllEquipmentTypes()
        {
            return _context.EquipmentTypes.ToList();
        }   
        
        public void DeleteEquipmentType(int id)
        {
            throw new NotImplementedException();
        }

        public EquipmentType AddEquipmentType(EquipmentType equipmentType)
        {
            _context.EquipmentTypes.Add(equipmentType);
            _context.SaveChanges();
            return equipmentType;
        }

        public EquipmentType UpdateEquipmentType(EquipmentType equipmentType)
        {
            throw new NotImplementedException();
        }
    }
}
