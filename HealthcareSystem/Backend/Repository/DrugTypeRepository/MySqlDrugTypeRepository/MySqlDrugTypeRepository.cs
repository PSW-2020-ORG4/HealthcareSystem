using Backend.Model;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.DrugTypeRepository.MySqlDrugTypeRepository
{
    public class MySqlDrugTypeRepository : IDrugTypeRepository
    {
        private readonly MyDbContext _context;
        public MySqlDrugTypeRepository(MyDbContext context)
        {
            _context = context;
        }
        public List<DrugType> GetAllDrugTypes()
        {
            return _context.DrugTypes.ToList();
        }
    }
}
