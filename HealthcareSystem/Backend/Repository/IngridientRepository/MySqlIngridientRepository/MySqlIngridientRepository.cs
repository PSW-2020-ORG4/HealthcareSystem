using Backend.Model;
using Backend.Repository.DrugTypeRepository;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.IngridientRepository.MySqlIngridientRepository
{
    public class MySqlIngridientRepository : IIngridientRepository
    {
        private readonly MyDbContext _context;
        public MySqlIngridientRepository(MyDbContext context)
        {
            _context = context;
        }
        public List<Ingredient> GetAllIngridients()
        {
            return _context.Ingridients.ToList();
        }
    }
}
