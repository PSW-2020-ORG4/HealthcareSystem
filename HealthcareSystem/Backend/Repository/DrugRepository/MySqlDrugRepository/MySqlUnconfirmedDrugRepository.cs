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
            Drug drug = GetDrug(id);
            _context.Remove(drug);
            _context.SaveChanges();
        }

        public List<Drug> GetAllDrugs()
        {
            return _context.Drugs.ToList();
        }

        public Drug GetDrug(int id)
        {
            return _context.Drugs.Find(id);
        }

        public int getLastId()
        {
            //TODO:Implement
            return 0;
        }

        public void AddDrug(Drug drug)
        {
            _context.Drugs.Add(drug);
            _context.SaveChanges();
        }

        public void UpdateDrug(Drug drug)
        {
            _context.Update(drug);
            _context.SaveChanges();
        }
    }
}
