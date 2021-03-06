﻿using Backend.Model;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository
{
    public class MySqlEquipmentRepository : IEquipmentRepository
    {
        private readonly MyDbContext _context;
        public MySqlEquipmentRepository(MyDbContext context)
        {
            _context = context;
        }
        public void DeleteEquipment(int id)
        {
            throw new NotImplementedException();
        }

        public List<Equipment> GetAllEquipment()
        {
            return _context.Equipment.ToList();
        }

        public Equipment GetEquipmentById(int id)
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
