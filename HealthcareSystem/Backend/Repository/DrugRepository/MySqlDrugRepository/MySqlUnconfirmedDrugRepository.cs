using Backend.Model;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.DrugRepository.MySQLDrugRepository
{
    public class MySqlUnconfirmedDrugRepository : IUnconfirmedDrugRepository
    {
        private readonly MyDbContext _context;
        public MySqlUnconfirmedDrugRepository(MyDbContext context)
        {
            _context = context;
        }
        public void DeleteDrug(int id)
        {
            throw new NotImplementedException();
        }

        public List<Drug> GetAllDrugs()
        {
            return _context.Drugs.ToList();
        }

        public Drug GetDrugById(int id)
        {
            return _context.Drugs.Find(id);
        }

        public int getLastId()
        {
            //TODO:Implement
            return 0;
        }

        public Drug AddDrug(Drug drug)
        {
            _context.Drugs.Add(drug);
            _context.SaveChanges();
            return drug;
        }

        public Drug SetDrug(Drug drug)
        {
            _context.Update(drug);
            _context.SaveChanges();
            return drug;
        }
    }
}
