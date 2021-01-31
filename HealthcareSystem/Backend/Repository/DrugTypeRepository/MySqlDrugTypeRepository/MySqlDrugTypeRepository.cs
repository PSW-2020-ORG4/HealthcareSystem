using Backend.Model;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository.DrugTypeRepository.MySqlDrugTypeRepository
{
    public class MySqlDrugTypeRepository : IDrugTypeRepository
    {
        private readonly MyDbContext _context;
        public MySqlDrugTypeRepository(MyDbContext context)
        {
            _context = context;
        }

        public DrugType AddDrugType(DrugType drugType)
        {
            _context.DrugTypes.Add(drugType);
            _context.SaveChanges();
            return drugType;
        }

        public void DeleteDrugType(int id)
        {
            throw new NotImplementedException();
        }

        public List<DrugType> GetAllDrugTypes()
        {
            return _context.DrugTypes.ToList();
        }

        public DrugType GetDrugType(int id)
        {
            return _context.DrugTypes.SingleOrDefault(x => x.Id == id);
        }

        public DrugType UpdateDrugType(DrugType drugType)
        {
            throw new NotImplementedException();
        }
    }
}
